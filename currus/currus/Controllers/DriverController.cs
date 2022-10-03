using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace currus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : Controller
    {
        [HttpGet]
        [Route("Drivers")]
        public ActionResult<List<DriverModel>> GetAllDrivers()
        {
            return Ok(JsonSerializer.Serialize(DriverRepository.drivers));
        }

        [Route("Adding")]
        [HttpPost]
        public string AddingDriver([FromBody] DriverModel driverModel)
        {
            DriverRepository.drivers.Add(driverModel);
            DriverRepository.drivers.Sort();
            return DriverRepository.drivers.Count.ToString();
        }
    }
}
