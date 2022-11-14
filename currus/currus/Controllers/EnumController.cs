using currus.Enums;
using currus.Logging.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace currus.Controllers;

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
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised when getting trip state enumerator. The following error occurred : " + ex.Message);
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
        catch (WebException ex) when ((ex.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.InternalServerError)
        {
            Logger.LogError("\r\nWebException InternalServerError protocol Raised when getting vehicle type enumerator. The following error occurred : " + ex.Message);
            return null;
        }
    }
}