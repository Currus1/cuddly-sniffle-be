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
        return Ok(JsonSerializer.Serialize(TripRepository.Trips));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddTrip([FromBody] Trip trip)
    {
        TripRepository.Trips.Add(trip);
        return TripRepository.Trips.Count.ToString();
    }
}