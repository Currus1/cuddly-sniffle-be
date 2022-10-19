using currus.Models;
using currus.Repository;
using Microsoft.AspNetCore.Mvc;

namespace currus.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserDbRepository _userDbRepository;

    public UserController(IUserDbRepository userDbRepository)
    {
        _userDbRepository = userDbRepository;
    }

    [HttpPost]
    [Route("Adding")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        await _userDbRepository.Add(user);
        await _userDbRepository.SaveAsync();
        return Ok(user);
    }
}