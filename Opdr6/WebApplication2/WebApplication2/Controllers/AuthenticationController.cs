using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticationController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto gebruikerLogin)
    {
        var _user = await _userManager.FindByNameAsync(gebruikerLogin.UserName);
        if (_user != null)
            if (await _userManager.CheckPasswordAsync(_user, gebruikerLogin.Password))
            {
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awef98awef978haweof8g7aw789efhh789awef8h9awh89efh89awe98f89uawef9j8aw89hefawef"));
        
                var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user.UserName) };
                var roles = await _userManager.GetRolesAsync(_user);
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: "https://localhost:8124",
                    audience: "https://localhost:8124",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(100),
                    signingCredentials: signingCredentials
                );
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions) });
            }
        
        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerToken)
    {
        ApplicationUser newUser = new ApplicationUser
        {
            UserName = registerToken.Username,
            Email = registerToken.Email,
            //password should be encrypted
            PasswordHash = registerToken.Password,
            Gender = (Gender) Enum.Parse(typeof(Gender), registerToken.Gender, true)
        };
        
        var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
        return !result.Succeeded ? new BadRequestObjectResult(result) : StatusCode(201);
    }
}

public class RegisterDto
{
    [Required] public string Username { get; init; }
    [Required] public string Email { get; init; }
    [Required] public string Password { get; init; }
    
    [Required] public string Gender { get; init; }
}

public class LoginDto
{
    [Required] public string UserName { get; init; }

    [Required] public string Password { get; init; }
}