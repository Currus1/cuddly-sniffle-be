using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserFileRepository _userFileRepository;

    public UserController(IUserFileRepository userFileRepository)
    {
        _userFileRepository = userFileRepository;
    }

    [HttpGet]
    [Route("Users/{id}")]
    public ActionResult<Driver> GetSingleUser(int id)
    {
        return Ok(_userFileRepository.Get(user => user.Id == id));
    }

    [HttpGet]
    [Route("Users")]
    public ActionResult<List<User>> GetAllUsers()
    {
        return Ok(JsonSerializer.Serialize(_userFileRepository.GetAll()));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddUser([FromBody] User user)
    {
        _userFileRepository.Add(user);
        _userFileRepository.Save();
        return _userFileRepository.GetAll().Count().ToString();
    }
}