using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserDbRepository _userDbRepository;

    public UserController(IUserDbRepository userDbRepository)
    {
        _userDbRepository = userDbRepository;
    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        try
        {
           await _userDbRepository.Add(user);
           await _userDbRepository.SaveAsync();
           return Ok(user);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised when adding a user. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteUser([FromBody] User user)
    {
        try
        {
           _userDbRepository.Delete(user);
           await _userDbRepository.SaveAsync();
           return Ok(user);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised when deleting a user. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
           _userDbRepository.DeleteById(id);
           await _userDbRepository.SaveAsync();
           return Ok(id);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised when deleting a user by ID. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        try
        {
           _userDbRepository.Update(user);
           await _userDbRepository.SaveAsync();
           return Ok(user);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            User? user = _userDbRepository.Get(id);
            if (user != null)
                return Ok(user);
            return Ok();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("/{id}/trip/{tripId}")]
    public async Task<IActionResult> SetRelation(int id, int tripId)
    {
        try
        {
           var user = _userDbRepository.SetRelation(id, tripId);
           _userDbRepository.Update(user);
           await _userDbRepository.SaveAsync();
           return Ok();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised. The following error occurred : " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("/{id}/trips")]
    public ICollection<Trip> GetAllTrips(int id)
    {
        return _userDbRepository.GetAllTrips(id);
    }

}