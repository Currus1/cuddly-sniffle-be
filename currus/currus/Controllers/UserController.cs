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
        try
        {
            _userFileRepository = userFileRepository;
        }
        catch (FileNotFoundException ex)
        {
            throw new FileNotFoundException("Could not find the requested file.", ex);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet]
    [Route("Users/{id}")]
    public ActionResult<Driver> GetSingleUser(int id)
    {
        return Ok(_userFileRepository.Get(predicate: user => user.Id == id));
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
        try
        {
            _userFileRepository.Add(user);
            _userFileRepository.Save();
            return _userFileRepository.GetAll().Count().ToString();
        }
        catch (FileNotFoundException ex)
        {
            throw new FileNotFoundException("Could not find the requested file.", ex);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}