using currus.Enums;
using Microsoft.AspNetCore.Mvc;


namespace currus.Controllers;

public class EnumController : Controller
{
    [HttpGet]
    [Route("TripStatus")]
    public String[] GetTripStateEnum()
    {
        return (String[])Enum.GetNames(typeof(TripStatuses));
    }

    [HttpGet]
    [Route("VehicleType")]
    public String[] GetVehicleTypeEnum()
    {
        return (String[])Enum.GetNames(typeof(VehicleTypes));
    }
}