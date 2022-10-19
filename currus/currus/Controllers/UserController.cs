﻿using currus.Models;
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

    [HttpDelete]
    [Route("Deletion")]
    public async Task<IActionResult> DeleteUser([FromBody] User user)
    {
        _userDbRepository.Delete(user);
        await _userDbRepository.SaveAsync();
        return Ok(user);
    }

    [HttpDelete]
    [Route("Deletion/{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        _userDbRepository.DeleteById(id);
        await _userDbRepository.SaveAsync();
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        _userDbRepository.Update(user);
        await _userDbRepository.SaveAsync();
        return Ok(user);
    }

    [HttpGet]
    [Route("{id}")]
    public User GetUser(int id)
    {
        User? user = _userDbRepository.Get(id);
        return user;
    }
}