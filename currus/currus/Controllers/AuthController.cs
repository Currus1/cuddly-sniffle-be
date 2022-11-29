using currus.Logging.Logic;
using currus.Models;
using currus.Models.DTOs;
using currus.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace currus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user_exist = await _userManager.FindByEmailAsync(userDto.Email);

                    if (user_exist != null)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "User already exists! Please log in"
                            }
                        });
                    }

                    var new_user = new User()
                    {
                        UserName = userDto.Name,
                        Surname = userDto.Surname,
                        Email = userDto.Email,
                        Birthdate = userDto.BirthDate,
                        PhoneNumber = userDto.Number
                    };

                    var is_created = await _userManager.CreateAsync(new_user, userDto.Password);
                    
                    if(is_created.Succeeded)
                    {
                        var token = GenerateJwtToken(new_user);

                        return Ok(new AuthResult()
                        {
                            Result = true,
                            Token = token
                        });
                    }

                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Server error."
                        },
                        Result = false
                    });
                }
                else
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Model invalid"
                        },
                        Result = false
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ": " + ex.StackTrace);
                return NotFound();
            } 
        }

        [HttpPost]
        [Route("login")]    
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user_exist = await _userManager.FindByEmailAsync(userDto.Email);

                    if (user_exist == null)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "User does not exist"
                            }
                        });
                    }

                    var isCorrect = await _userManager.CheckPasswordAsync(user_exist, userDto.Password);

                    if(!isCorrect)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Errors = new List<string>()
                            {
                                "Invalid Credentials"
                            },
                            Result = false
                        });
                    }

                    var jwtToken = GenerateJwtToken(user_exist);

                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = jwtToken
                    });

                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Server error"
                        },
                        Result = false
                    });
                }
                else
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                        {
                            "Model invalid"
                        },
                        Result = false
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message + ": " + ex.StackTrace);
                return NotFound();
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

    }
}
 