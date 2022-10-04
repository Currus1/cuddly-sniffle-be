using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class DriverController : Controller
{
    private readonly IDriverRepository _driverRepository;

    public DriverController(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    [HttpGet]
    [Route("Drivers")]
    public ActionResult<List<Driver>> GetAllDrivers()
    {
        return Ok(_driverRepository.GetAll());
    }

    [Route("Adding")]
    [HttpPost]
    public string AddingDriver([FromBody] Driver driverModel)
    {
        _driverRepository.Add(driverModel);
        _driverRepository.Save();
        return _driverRepository.GetAll().Count().ToString();
    }
}