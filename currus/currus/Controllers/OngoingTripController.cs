using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace currus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OngoingTripController : Controller
    {
        [HttpGet]
        [Route("trips")]
        public List<OngoingTrip> Get()
        {
            return TripRepository.ongoingTrips;
        }

        [HttpPost]
        [Route("adding")]
        public void AddUser([FromBody] OngoingTrip ongoingTrip)
        {
            TripRepository.ongoingTrips.Add(ongoingTrip);
        }
    }
}
