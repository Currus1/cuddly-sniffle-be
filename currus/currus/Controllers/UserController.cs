using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("Users")]
    public ActionResult<List<User>> GetAllUsers()
    {
        return Ok(JsonSerializer.Serialize(_userRepository.GetAll()));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddUser([FromBody] User user)
    {
        _userRepository.Add(user);
        _userRepository.Save();
        return _userRepository.GetAll().Count().ToString();
    }
}