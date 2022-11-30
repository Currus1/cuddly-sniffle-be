using currus.Enums;
using currus.Logging.Logic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace currus.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class EnumController : Controller
{
    [HttpGet]
    [Route("TripStatus")]
    public String[]? GetTripStateEnum()
    {
        try
        {
            return (String[])Enum.GetNames(typeof(TripStatuses));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return null;
        }
    }

    [HttpGet]
    [Route("VehicleType")]
    public String[]? GetVehicleTypeEnum()
    {
        try
        {
           return (String[])Enum.GetNames(typeof(VehicleTypes));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message + ": " + ex.StackTrace);
            return null;
        }
    }
}