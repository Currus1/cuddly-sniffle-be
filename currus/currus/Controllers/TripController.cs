using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripFileRepository _tripFileRepository;

    public TripController(ITripFileRepository tripFileRepository)
    {
        _tripFileRepository = tripFileRepository;
    }

    [HttpGet]
    [Route("Trips")]
    public ActionResult<List<Trip>> GetAllTrips()
    {
        return Ok(JsonSerializer.Serialize(_tripFileRepository.GetAll()));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddTrip([FromBody] Trip trip)
    {
        int basePrice = trip.CalculateBasePrice(trip); 
        trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);
        _tripFileRepository.Add(trip);
        _tripFileRepository.Save();
        return _tripFileRepository.GetAll().Count().ToString();
    }
}