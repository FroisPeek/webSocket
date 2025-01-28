using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wsapi.Context;
using wsapi.Models;

namespace wsapi.Controllers;

[Route("api/Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserController> _logger;

    public UserController(ApplicationDbContext context, ILogger<UserController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = new Response<List<Users>>();

        try
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                response.Success = false;
                response.Message = "No users found";
                return NotFound(response);
            }

            response.Success = true;
            response.Message = "All users found";
            response.Data = users;
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            response.Success = false;
            response.Message = ex.Message;
            return BadRequest(response);
        }
    }
}