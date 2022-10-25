using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

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
        await _tripDbRepository.Add(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteTrip([FromBody] Trip trip)
    {
        _tripDbRepository.Delete(trip);
        await _tripDbRepository.SaveAsync();
        return Ok(trip);
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        _tripDbRepository.DeleteById(id);
        await _tripDbRepository.SaveAsync();
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTrip([FromBody] Trip trip)
    {
        _tripDbRepository.Update(trip);
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
    public List<Trip> GetTrips(string tripStatus)
    {
        List<Trip>? trips = _tripDbRepository.GetAllByStatus(tripStatus).ToList();
        return trips;
    }
}