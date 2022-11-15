using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripDbRepository _tripDbRepository;

    public TripController(ITripDbRepository tripDbRepository)
    {
        _tripDbRepository = tripDbRepository;
    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddTrip([FromBody] Trip trip)
    {
        try
        {
            int basePrice = trip.CalculateBasePrice(trip);
            trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);
            await _tripDbRepository.Add(trip);
            await _tripDbRepository.SaveAsync();
            return Ok(trip);
        } 
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when adding a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when adding a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when adding a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteTrip([FromBody] Trip trip)
    {
        try
        {
           _tripDbRepository.Delete(trip);
           await _tripDbRepository.SaveAsync();
           return Ok(trip);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when deleting a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when deleting a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when deleting a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        try
        {
           _tripDbRepository.DeleteById(id);
           await _tripDbRepository.SaveAsync();
           return Ok(id);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when deleting a trip by ID. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when deleting a trip by ID. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when deleting a trip by ID. The following error occurred : " + ex.Message);
            return NotFound();
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTrip([FromBody] Trip trip)
    {
        try
        {
           _tripDbRepository.Update(trip);
           await _tripDbRepository.SaveAsync();
           return Ok(trip);
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised(error code: 500) when updating a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InsufficientStorage)
        {
            Logger.LogError("\r\nWebException InsufficientStorage protocol Raised(error code: 507) when updating a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.ServiceUnavailable)
        {
            Logger.LogError("\r\nWebException ServiceUnavailable protocol Raised(error code: 503) when updating a trip. The following error occurred : " + ex.Message);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public Trip GetTrip(int id)
    {
        Trip? trip = _tripDbRepository.Get(id);
        return trip;
    }

    [HttpGet]
    [Route("Trips")]
    public IEnumerable<Trip> GetTrips(string tripStatus)
    {
        return _tripDbRepository.GetAllByStatus(tripStatus);
    }
}