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
            return Ok(JsonSerializer.Serialize(TripManager.trips));
        }

        [Route("Adding")]
        [HttpPost]
        public string AddingTrip([FromBody] TripModel tripModel)
        {
            TripManager.trips.Add(tripModel);
            return TripManager.trips.Count.ToString();
        }
    }
}
