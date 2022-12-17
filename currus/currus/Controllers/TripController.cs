using currus.Logging.Logic;
using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using currus.Models.DTOs;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("apisecure/[controller]")]
[ApiController]
public class TripController : Controller
{
    private readonly ITripDbRepository _tripDbRepository;
    private readonly UserManager<User> _userManager;

    public TripController(ITripDbRepository tripDbRepository, UserManager<User> userManager)
    {
        _tripDbRepository = tripDbRepository;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("UserAdd")]
    public async Task<IActionResult> AddUserToTrip()
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if (existingUser == null)
            {
                return BadRequest();
            }

            var tripId = HttpContext.Request.Query["tripId"];
            var trip = _tripDbRepository.GetTripAsNotTracked(Int32.Parse(tripId));

            if(trip.Users == null)
            {
                trip.Users = new List<User>();
            }
            if(!ValidateTripAvailability(trip))
            {
                return Forbid();
            }
            if (trip.Users.Any(u => u.Email == existingUser.Email))
            {
                return BadRequest(trip.Users.Count);
            }

            bool isAdded = _tripDbRepository.AddUserToTrip(existingUser, trip.Id);
            if (isAdded)
            {
                await _tripDbRepository.SaveAsync();
                return Ok(trip.Users.Count);
            }
            return BadRequest();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddTrip([FromBody] TripAddDto tripDto)
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if (existingUser == null)
            {
                return BadRequest();
            }
            Trip trip = new Trip(tripDto.SLatitude, tripDto.SLongitude,
                                 tripDto.DLatitude, tripDto.DLongitude,
                                 tripDto.StartingPoint, tripDto.Destination,
                                 tripDto.EstimatedTripPrice,
                                 tripDto.Seats, tripDto.TripDate);

            trip.Distance = 10; // Fixed
            trip.Hours = 1; // Fixed
            trip.Minutes = 1; // Fixed
            trip.TripStatus = "Planned";
            if(existingUser.VehicleType != null)
            {
                trip.VehicleType = existingUser.VehicleType;
            }
            trip.DriverId = existingUser.Id;
            trip.Users = new List<User>();

            await _tripDbRepository.Add(trip);
            await _tripDbRepository.SaveAsync();
            return Ok(trip);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
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
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteTripById(int id)
    {
        try
        {
            var value = _tripDbRepository.Get(id);
            if (value != null)
            {
                Trip trip = value;
                _tripDbRepository.DeleteById(id);
                await _tripDbRepository.SaveAsync();
                return Ok(id);
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }
    
    [HttpGet]
    [Route("Driver")]
    public async Task<IActionResult> GetTripDriver()
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if (existingUser == null)
            {
                return BadRequest();
            }
            var id = HttpContext.Request.Query["driverId"];
 
            var driver = await _userManager.FindByIdAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetTripsForUser()
    {
        try
        {
            if (HttpContext == null)
                return BadRequest();

            var email = HttpContext.Items["email"];
            if (email == null)
            {
                return BadRequest();
            }

            var existingUser = await _userManager.FindByEmailAsync(email.ToString());
            if (existingUser == null)
            {
                return BadRequest();
            }

            var trips = _tripDbRepository.GetTripsForUser(existingUser.Id);
            if (trips != null)
            {
                return Ok(trips);
            } 
            return Ok();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return NotFound();
        }
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetTrip(int id)
    {
        try
        {
            var trip = _tripDbRepository.Get(id);
            if (trip != null)
                return Ok(trip);
            return BadRequest();
        } 
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + " " + ex.StackTrace);
            return NotFound();
        }
        
    }

    [HttpGet]
    [Route("Trips")]
    public async Task<List<TripExportDto>> GetTrips(string tripStatus)
    {
        List<Trip> list = _tripDbRepository.GetAllByStatus(tripStatus).ToList();
        List<TripExportDto> returnList = new List<TripExportDto>();
        foreach (Trip trip in list)
        {
            var occupied = _tripDbRepository.GetAllUsers(trip.Id);
            User user = await _userManager.FindByIdAsync(trip.DriverId);
            if (user != null)
            {
                TripExportDto tripDto =
                    new TripExportDto(
                    trip.Id, trip.SLatitude,
                    trip.SLongitude, trip.DLatitude,
                    trip.DLongitude, trip.StartingPoint,
                    trip.Destination, occupied.Count,
                    trip.Seats, trip.DriverId,
                    user.Name, user.Surname,
                    trip.VehicleType, trip.EstimatedTripPrice,
                    trip.TripStatus, trip.TripDate);
                if (tripDto != null)
                {
                    returnList.Add(tripDto);
                }
            }
            
        }

        return returnList;
    }

    [HttpGet]
    [Route("{id}/users")]
    public ICollection<User> GetAllUsers(int id)
    {
        return _tripDbRepository.GetAllUsers(id);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public bool ValidateTripAvailability(Trip trip)
    {
        if (trip.Users != null)
        {
            if (trip.Users.Count < trip.Seats)
            {
                return true;
            }
        }
        return false;
    }
}
