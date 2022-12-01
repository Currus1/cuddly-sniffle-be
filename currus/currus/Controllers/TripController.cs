using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("apisecure/[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripDbRepository _tripDbRepository;

    public TripController(ITripDbRepository tripDbRepository)
    {
        _tripDbRepository = tripDbRepository;
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