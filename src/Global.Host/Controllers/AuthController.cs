using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Global.Host.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("token")]
    public ActionResult<TokenResponse> CreateToken([FromBody] TokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            return BadRequest();
        }

        var key = _configuration["Jwt:Key"] ?? "dev-key-change-me";
        var issuer = _configuration["Jwt:Issuer"] ?? "Global.Api";
        var audience = _configuration["Jwt:Audience"] ?? "Global.Api";
        var expiresMinutes = int.TryParse(_configuration["Jwt:ExpiresMinutes"], out var minutes) ? minutes : 60;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.Username),
            new(ClaimTypes.Name, request.Username)
        };

        if (!string.IsNullOrWhiteSpace(request.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, request.Role));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: credentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new TokenResponse
        {
            AccessToken = tokenValue,
            ExpiresAtUtc = token.ValidTo
        });
    }
}

public class TokenRequest
{
    public string Username { get; set; } = string.Empty;
    public string? Role { get; set; }
}

public class TokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
}
