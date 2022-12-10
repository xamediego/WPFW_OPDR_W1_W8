using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController
{
    private readonly DatabaseContext _context;

        
    public UserController(DatabaseContext context)
    {
        _context = context;
    }
    
    [Authorize(Roles = "Employee")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

}