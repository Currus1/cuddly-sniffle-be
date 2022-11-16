﻿using currus.Events;
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
    private readonly UserStatusChanger _userStatusChanger;

    public TripController(ITripDbRepository tripDbRepository)
    {
        _tripDbRepository = tripDbRepository;
        _userStatusChanger = new UserStatusChanger();

    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddTrip([FromBody] Trip trip)
    {
        int basePrice = trip.CalculateBasePrice(trip); 
        trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);

        _userStatusChanger.ChangeEventHandler(trip, true);

        
        if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
            _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(true));
        else
            _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(false));
        _userStatusChanger.ChangeEventHandler(trip, false);

        await _tripDbRepository.Add(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteTrip([FromBody] Trip trip)
    {
        _userStatusChanger.ChangeEventHandler(trip, true);

        _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(false));
        _userStatusChanger.ChangeEventHandler(trip, false);

        _tripDbRepository.Delete(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        Trip trip = _tripDbRepository.Get(id);
        _userStatusChanger.ChangeEventHandler(trip, true);

        _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(false));
        _userStatusChanger.ChangeEventHandler(trip, false);

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

        _userStatusChanger.ChangeEventHandler(oldTrip, true);
        _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(false));
        _userStatusChanger.ChangeEventHandler(oldTrip, false);
        _userStatusChanger.ChangeEventHandler(trip, true);

        if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
        {
            _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(true));
        }
        else if (trip.TripStatus == "Ended" || trip.TripStatus == "Cancelled")
        {
            _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(false));
        }
        _userStatusChanger.ChangeEventHandler(trip, false);

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

        _userStatusChanger.ChangeEventHandler(trip, true);

        if (trip.TripStatus == "Planned" || trip.TripStatus == "Ongoing")
        {
            _userStatusChanger.OnStatusChanged(this, new UserStatusEventArgs(true));
        }

        _userStatusChanger.ChangeEventHandler(trip, false);

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