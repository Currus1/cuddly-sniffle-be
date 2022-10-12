using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class TripController : Controller
{
    [HttpGet]
    [Route("Trips")]
    public ActionResult<List<Trip>> GetAllTrips()
    {
        TripListRepository.Trips.Sort();
        return Ok(JsonSerializer.Serialize(TripListRepository.Trips));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddTrip([FromBody] Trip trip)
    {
        int basePrice = trip.CalculateBasePrice(trip); 
        trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);
        TripListRepository.Trips.Add(trip);
        return TripListRepository.Trips.Count.ToString();
    }
}