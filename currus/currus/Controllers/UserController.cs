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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<UserModel>> GetAllUsers()
        {
            return Ok(JsonSerializer.Serialize(UserRepository.Users));
        }

        [HttpPost]
        [Route("Adding")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string AddUser([FromBody] UserModel user)
        {
            UserRepository.Users.Add(user);
            UserRepository.Users.Sort();
            return UserRepository.Users.Count.ToString();
        }
    }
}