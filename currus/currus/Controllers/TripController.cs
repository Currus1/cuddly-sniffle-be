using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace currus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripController : Controller
    {
        [HttpGet]
        [Route("Trips")]
        public ActionResult<List<TripModel>> GetAllTrips()
        {
            return Ok(JsonSerializer.Serialize(TripRepository.Trips));
        }

        [HttpPost]
        [Route("Adding")]
        public string AddTrip([FromBody] TripModel trip)
        {
            TripRepository.Trips.Add(trip);
            return TripRepository.Trips.Count.ToString();
        }
    }
}
