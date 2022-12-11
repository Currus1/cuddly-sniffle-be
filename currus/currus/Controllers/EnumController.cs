using currus.Enums;
using currus.Logging.Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("apisecure/[controller]")]
public class EnumController : Controller
{
    [HttpGet]
    [Route("TripStatus")]
    public string[]? GetTripStateEnum()
    {
        try
        {
            return Enum.GetNames(typeof(TripStatuses));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return null;
        }
    }

    [HttpGet]
    [Route("VehicleType")]
    public string[]? GetVehicleTypeEnum()
    {
        try
        {
           return Enum.GetNames(typeof(VehicleTypes));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return null;
        }
    }
}