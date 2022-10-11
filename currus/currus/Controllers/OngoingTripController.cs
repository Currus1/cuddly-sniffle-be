using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[ApiController]
[Route("[controller]")]
public class OngoingTripController : Controller
{
    [HttpGet]
    [Route("All")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<List<OngoingTrip>> Get()
    {
        return Ok(JsonSerializer.Serialize(TripListRepository.ongoingTrips));
    }

    [HttpPost]
    [Route("Adding")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public void Add([FromBody] OngoingTrip ongoingTrip)
    {
        TripListRepository.ongoingTrips.Add(ongoingTrip);
    }
}