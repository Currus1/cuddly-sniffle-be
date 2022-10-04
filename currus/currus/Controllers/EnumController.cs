using Microsoft.AspNetCore.Mvc;
using static currus.Enums.Enumerators;

namespace currus.Controllers;

public class EnumController : Controller
{
    [HttpGet]
    [Route("TripStatus")]
    public TripStatus[] GetTripStateEnum()
    {
        return (TripStatus[])Enum.GetValues(typeof(TripStatus)).Cast<TripStatus>();
    }

    [HttpGet]
    [Route("VehicleType")]
    public VehicleTypes[] GetVehicleTypeEnum()
    {
        return (VehicleTypes[])Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>();
    }
}