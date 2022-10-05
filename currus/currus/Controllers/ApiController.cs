using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IConfiguration _iConfiguration;

        public ApiController(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        [HttpGet]
        [Route("Secrets/ApiKey")]
        public string GetApiKey()
        {
            return _iConfiguration.GetValue<string>("ApiKey");
        }

    }
}
