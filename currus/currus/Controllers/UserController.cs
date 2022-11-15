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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when adding a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when adding a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when adding a user. The following error occurred : " + ex.Message);
            return NotFound();
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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when deleting a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when deleting a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when deleting a user. The following error occurred : " + ex.Message);
            return NotFound();
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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when deleting a user by ID. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when deleting a user by ID. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when deleting a user by ID. The following error occurred : " + ex.Message);
            return NotFound();
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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(erro code: 500) when updating a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when updating a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when updating a user. The following error occurred : " + ex.Message);
            return NotFound();
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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when getting a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when getting a user. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when getting a user. The following error occurred : " + ex.Message);
            return NotFound();
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
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when setting relation. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when setting relation. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when setting relation. The following error occurred : " + ex.Message);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("/{id}/trips")]
    public ICollection<Trip> GetAllTrips(int id)
    {
        return _userDbRepository.GetAllTrips(id);
    }

}