using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("apisecure/[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripDbRepository _tripDbRepository;
    private readonly UserManager<User> _userManager;

    public TripController(ITripDbRepository tripDbRepository, UserManager<User> userManager)
    {
        _tripDbRepository = tripDbRepository;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddTrip([FromBody] Trip trip)
    {
        try
        {
            int basePrice = trip.CalculateBasePrice(trip); 
            trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);

            await _tripDbRepository.Add(trip);
            await _tripDbRepository.SaveAsync();
            return Ok(trip);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteTrip([FromBody] Trip trip)
    {
        try
        {
            _tripDbRepository.Delete(trip);
            await _tripDbRepository.SaveAsync();
            return Ok(trip);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        try
        {
            Trip trip = _tripDbRepository.Get(id);
            _tripDbRepository.DeleteById(id);
            await _tripDbRepository.SaveAsync();
            return Ok(id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }
    
    [HttpGet]
    [Route("Driver")]
    public async Task<IActionResult> GetTripDriver()
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
            var id = HttpContext.Request.Query["driverId"];
 
            var driver = await _userManager.FindByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetTripsForUser()
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

            var trips = _tripDbRepository.GetTripsForUser(existingUser.Id);
            if (trips != null)
            {
                return Ok(trips);
            } 
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
    public async Task<IActionResult> UpdateTrip([FromBody] Trip trip)
    {
        try
        {
            var oldTrip = _tripDbRepository.GetTripAsNotTracked(trip.Id); 
            _tripDbRepository.Update(trip);
            await _tripDbRepository.SaveAsync();
            return Ok(trip);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTrip(int id)
    {
        try
        {
            Trip? trip = _tripDbRepository.Get(id);
            if (trip != null)
                return Ok(trip);
            return Ok();
        } 
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + " " + ex.StackTrace);
            return NotFound();
        }
        
    }

    [HttpGet]
    [Route("Trips")]
    public IEnumerable<Trip> GetTrips(string tripStatus)
    {
        return _tripDbRepository.GetAllByStatus(tripStatus);
    }

    [HttpPut]
    [Route("{id}/user/{userId}")]
    public async Task<IActionResult> SetRelation(int id, int userId)
    {
        var trip = _tripDbRepository.SetRelation(id, userId);
        _tripDbRepository.Update(trip);
        await _tripDbRepository.SaveAsync();
        return Ok();
    }

    [HttpGet]
    [Route("{id}/users")]
    public ICollection<User> GetAllUsers(int id)
    {
        return _tripDbRepository.GetAllUsers(id);
    }
}