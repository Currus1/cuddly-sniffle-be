using System.Text.Json;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripFileRepository _tripFileRepository;

    public TripController(ITripFileRepository tripFileRepository)
    {
        try
        {
        _tripFileRepository = tripFileRepository;
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
    [Route("Trips")]
    public ActionResult<List<Trip>> GetAllTrips()
    {
        return Ok(JsonSerializer.Serialize(_tripFileRepository.GetAll()));
    }

    [HttpPost]
    [Route("Adding")]
    public string AddTrip([FromBody] Trip trip)
    {
        try
        {
            int basePrice = trip.CalculateBasePrice(trip);
            trip.EstimatedTripPrice = trip.CalculateTripPrice(trip.Hours, trip.Minutes, trip.Distance, basePrice);
            _tripFileRepository.Add(trip);
            _tripFileRepository.Save();
            return _tripFileRepository.GetAll().Count().ToString();
        }
        catch (ArgumentOutOfRangeException ex) when (trip.Hours <= 0 && trip.Minutes <= 0)
        {
            throw new ArgumentOutOfRangeException("Trip duration is not valid.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (trip.Distance <= 0)
        {
            throw new ArgumentOutOfRangeException("Trip distance is not valid.", ex);
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