using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class DriverController : Controller
{
    private readonly IDriverFileRepository _driverFileRepository;

    public DriverController(IDriverFileRepository driverFileRepository)
    {
        _driverFileRepository = driverFileRepository;
    }

    [HttpGet]
    [Route("Drivers/{id}")]
    public ActionResult<Driver> GetSingleDriver(int id)
    {
        return Ok(_driverFileRepository.Get(predicate: driver => driver.Id == id));
    }

    [HttpGet]
    [Route("Drivers")]
    public ActionResult<List<Driver>> GetAllDrivers()
    {
        return Ok(_driverFileRepository.GetAll());
    }

    [Route("Adding")]
    [HttpPost]
    public string AddingDriver([FromBody] Driver driverModel)
    {
        _driverFileRepository.Add(_driverFileRepository.CheckVehicleType(driver: driverModel));
        _driverFileRepository.Save();
        return _driverFileRepository.GetAll().Count().ToString();
    }
}