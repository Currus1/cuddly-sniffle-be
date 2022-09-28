using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace currus.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet]
        [Route("Users")]
        public ActionResult<List<UserModel>> GetAllUsers()
        {
            return Ok(JsonSerializer.Serialize(UserManager.Users));
        }

        [HttpPost]
        [Route("Adding")]
        public string AddUser([FromBody] UserModel user)
        {
            UserManager.Users.Add(user);
            return UserManager.Users.Count.ToString();
        }
    }
}