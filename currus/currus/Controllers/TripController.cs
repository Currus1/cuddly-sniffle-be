using currus.Events;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace currus.Controllers;

[Route("[controller]")]
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
        int basePrice = trip.CalculateBasePrice(trip); 
        trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);

        UserStatusEventHandler.ChangeEventHandler(trip, true);

        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
                UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(true));
            else
                UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(false));
            UserStatusEventHandler.ChangeEventHandler(trip, false);
        }

        await _tripDbRepository.Add(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteTrip([FromBody] Trip trip)
    {
        UserStatusEventHandler.ChangeEventHandler(trip, true);

        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(false));
            UserStatusEventHandler.ChangeEventHandler(trip, false);
        }

        _tripDbRepository.Delete(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        Trip trip = _tripDbRepository.Get(id);
        UserStatusEventHandler.ChangeEventHandler(trip, true);

        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(false));
            UserStatusEventHandler.ChangeEventHandler(trip, false);
        }

        _tripDbRepository.DeleteById(id);
        await _tripDbRepository.SaveAsync();
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTrip([FromBody] Trip trip)
    {
        var oldTrip = _tripDbRepository.GetTripAsNotTracked(trip.Id); 
        _tripDbRepository.Update(trip);

        UserStatusEventHandler.ChangeEventHandler(oldTrip, true);
        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(false));
            UserStatusEventHandler.ChangeEventHandler(oldTrip, false);
        }
        UserStatusEventHandler.ChangeEventHandler(trip, true);

        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
            {
                UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(true));
            }
            else if (trip.TripStatus == "Ended" || trip.TripStatus == "Cancelled")
            {
                UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(false));
            }
            UserStatusEventHandler.ChangeEventHandler(trip, false);
        }

        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpGet]
    [Route("{id}")]
    public Trip GetTrip(int id)
    {
        Trip? trip = _tripDbRepository.Get(id);
        return trip;
    }

    [HttpGet]
    [Route("Trips")]
    public IEnumerable<Trip> GetTrips(string tripStatus)
    {
        return _tripDbRepository.GetAllByStatus(tripStatus);
    }

    [HttpPut]
    [Route("/{id}/user/{userId}")]
    public async Task<IActionResult> SetRelation(int id, int userId)
    {
        var trip = _tripDbRepository.SetRelation(id, userId);
        _tripDbRepository.Update(trip);

        UserStatusEventHandler.ChangeEventHandler(trip, true);

        if (UserStatusEventHandler.UserStatusChanged != null)
        {
            if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
            {
                UserStatusEventHandler.OnUserStatusChanged(this, new UserStatusEventArgs(true));
            }
            UserStatusEventHandler.ChangeEventHandler(trip, false);
        }

        await _tripDbRepository.SaveAsync();
        return Ok();
    }

    [HttpGet]
    [Route("/{id}/users")]
    public ICollection<User> GetAllUsers(int id)
    {
        return _tripDbRepository.GetAllUsers(id);
    }
}