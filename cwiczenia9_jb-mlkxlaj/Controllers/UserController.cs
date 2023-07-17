using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zadanie9.DTOs;
using Zadanie9.Models;


namespace Zadanie9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly s24427Context _context;

    public UserController(s24427Context context)
    {
        _context = context;
    }
    
    [HttpPost("/Register")]
    public IActionResult RegisterUser(UserDTO userRdto)
    {
        var salt = CreateSalt();
        
        var User = new User
        {
            Login = userRdto.Login,
            Password = CreateHash(userRdto.Password, salt),
            Salt = salt
        };

         _context.Users.AddAsync(User);
         _context.SaveChanges();
        
        return Ok();
    }

    [HttpPost("/Login")]
    public IActionResult LoginUser(UserDTO userDto)
    {
        var user =  _context.Users.FirstOrDefault(a => a.Login == userDto.Login);
        
        if (user == null)
        {
            return NotFound("User with this Login doesnt exists");
        }

        if (!user.Password.Equals(CreateHash(userDto.Password, user.Salt)))
        {
            return BadRequest("Wrong password");
        }

        var accessToken = GetToken(user.IdUser.ToString(), user.Login, "toBedzieTajneKodowanie");

        var refreshToken = GetToken(user.IdUser.ToString(), user.Login, "toBedzieRefreshKodowanie");
        user.RefreshToken = refreshToken;

        _context.SaveChanges();
        
        return Ok("AT\t" + accessToken + "\nRT\t" + refreshToken);
    }

    [HttpPost("/RefreshToken")]
    public IActionResult RefreshToken(string refreshToken)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshToken.Equals(refreshToken));

        if (user == null)
        {
            return BadRequest();
        }
        
        var accessT = GetToken(user.IdUser.ToString(), user.Login, "toBedzieTajneKodowanie");

        var refreshT = GetToken(user.IdUser.ToString(), user.Login, "toBedzieRefreshKodowanie");
        user.RefreshToken = refreshT;

        _context.SaveChanges();
        
        return Ok("AT\t" + accessT + "\nRT\t" + refreshT);
    }

    private static string CreateHash(string value, string salt)
    {
        var valueBytes = KeyDerivation.Pbkdf2(password: value,
            salt: Encoding.UTF8.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8);

        return Convert.ToBase64String(valueBytes);
    }

    private static string CreateSalt()
    {
        byte[] randomBytes = new Byte[128 / 8];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }

    private static string GetToken(string idUser, string login,string type)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, idUser),
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, "user")
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(type));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken("Zadanie9", "User",
            claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
        
    
}