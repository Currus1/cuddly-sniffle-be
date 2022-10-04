using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : Controller
{
    [HttpGet]
    public string Get()
    {
        return "Sveiki visi ka jus";
    }
}