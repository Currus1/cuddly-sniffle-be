using Microsoft.AspNetCore.Mvc;
using static currus.Enums.Enumerators;

namespace currus.Controllers
{
    public class EnumController : Controller
    {
        public TripStatus tripStatus;

        [HttpGet]
        [Route("TripStatus")]
        public TripStatus[] GetEnum()
        {
            return (TripStatus[])Enum.GetValues(typeof(TripStatus)).Cast<TripStatus>();
        }
    }
}
