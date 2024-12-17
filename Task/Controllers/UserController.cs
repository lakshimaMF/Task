using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Task.Models;
using Task.UnitOfWork;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (await _context.User.AnyAsync(u => u.Email == user.Email))
        {
            return BadRequest("Email already exists.");
        }

        _context.User.Add(user);
        await _context.SaveChangesAsync();


        return Ok("User registered successfully.");
    }

    [HttpGet("Users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = _context.User.ToList();
        //_context.Task.FindAsync(t => t.userId==User.)
        return Ok(users);

    }

    private string HashPassword(string password)
    {
        byte[] salt = new byte[16];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256, 
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashedPassword;
    }
}
