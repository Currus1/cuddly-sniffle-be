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

    [HttpGet]
    [Route("Trips")]
    public ActionResult<List<Trip>> GetAllTrips()
    {
        return Ok(JsonSerializer.Serialize(_tripDbRepository.GetAll()));
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
}