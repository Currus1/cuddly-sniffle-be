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
            return Ok(JsonSerializer.Serialize(DriverManager.drivers));
        }

        [Route("Adding")]
        [HttpPost]
        public string AddingDriver([FromBody] DriverModel driverModel)
        {
            DriverManager.drivers.Add(driverModel);
            return DriverManager.drivers.Count.ToString();
        }
    }
}
