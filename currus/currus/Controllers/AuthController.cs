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
using System.Text.RegularExpressions;

namespace currus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IConfiguration configuration)
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
                    string emailPattern = @"^([a-zA-Z0-9_\-\.]+)@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$";
                    string phoneNumberPattern = @"^((86|\+3706)\d{7})$";
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
                    if (!Regex.IsMatch(userDto.Email, emailPattern, RegexOptions.IgnoreCase) || !Regex.IsMatch(userDto.PhoneNumber.ToString(), phoneNumberPattern, RegexOptions.IgnoreCase))
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Email or Phone number do not match the required format!"
                            }
                        });
                    }
                    var new_user = new User()
                    {
                        Name = userDto.Name,
                        Surname = userDto.Surname,
                        Email = userDto.Email,
                        Birthdate = userDto.Birthdate,
                        PhoneNumber = userDto.PhoneNumber,
                        UserName = userDto.Email
                    };

                    var is_created = await _userManager.CreateAsync(new_user, userDto.Password);

                    if (is_created.Succeeded)
                    {
                        return Ok(new AuthResult()
                        {
                            Errors = new List<string>()
                            {
                                "User Registration Success!"
                            },
                            Result = true
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
                    if (jwtToken != null)
                    {
                        return Ok(new AuthResult()
                        {
                            Result = true,
                            Token = jwtToken
                        });
                    }
                    

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
 