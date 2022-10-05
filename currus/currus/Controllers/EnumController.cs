using currus.Enums;
using Microsoft.AspNetCore.Mvc;


namespace currus.Controllers;

public class EnumController : Controller
{
    [HttpGet]
    [Route("TripStatus")]
    public TripStatuses[] GetTripStateEnum()
    {
        return (TripStatuses[])Enum.GetValues(typeof(TripStatuses)).Cast<TripStatuses>();
    }

    [HttpGet]
    [Route("VehicleType")]
    public VehicleTypes[] GetVehicleTypeEnum()
    {
        return (VehicleTypes[])Enum.GetValues(typeof(VehicleTypes)).Cast<VehicleTypes>();
    }
}