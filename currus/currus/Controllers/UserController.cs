using currus.Logging.Logic;
using currus.Models;
using currus.Models.DTOs;
using currus.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("apisecure/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITripDbRepository _tripDbRepository;

    public UserController(UserManager<User> userManager, IConfiguration configuration, ITripDbRepository tripDbRepository)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tripDbRepository = tripDbRepository;
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteUser()
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(email.ToString());
            var deletedUser = await _userManager.DeleteAsync(user);
            if(deletedUser.Succeeded)
            {
                return Ok();
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }


    [HttpPut]
    [Route("Driver")]
    public async Task<IActionResult> UpdateUserAsDriver([FromBody] DriverDto driver)
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if(existingUser == null)
            {
                return BadRequest();
            }

            existingUser.LicenseNumber = driver.LicenseNumber;
            existingUser.VehicleType = driver.VehicleType;
            existingUser.DriversLicense = driver.DriversLicense;

            await _userManager.UpdateAsync(existingUser);

            return Ok();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            await _userManager.UpdateAsync(user);

            return BadRequest();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpGet()]
    [Route("")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if(email == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(email.ToString());

            if (user != null)
            {
                UserRegisterDto userDto = new UserRegisterDto
                (
                    user.Name,
                    user.Surname,
                    user.Email,
                    user.Birthdate,
                    user.PhoneNumber
                );
                return Ok(userDto);
            }   
  
            return BadRequest();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + " " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpPut]
    [Route("trip/{tripId}")]
    public async Task<IActionResult> SetRelation(int tripId)
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if (existingUser == null)
            {
                return BadRequest();
            }
            if (existingUser.Trips == null)
            {
                existingUser.Trips = new List<Trip>();
            }
            var trip = _tripDbRepository.Get(tripId);
            if (trip != null)
            {
                existingUser.Trips?.Add(trip);
                await _userManager.UpdateAsync(existingUser);
                return Ok();
            }
            
            return BadRequest();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + " " + ex.StackTrace);
            return NotFound();
        }
    }
}