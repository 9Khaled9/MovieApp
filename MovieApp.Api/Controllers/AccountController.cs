using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Redirect user to Google or Azure AD B2C for login
        [HttpGet("login/{provider}")]
        public IActionResult Login(string provider)
        {
            if (provider != "Google" && provider != "AzureAdB2C")
            {
                return BadRequest("Provider not supported");
            }

            var redirectUrl = Url.Action(nameof(ExternalLoginCallback));
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider);
        }

        // Callback after successful login
        [HttpGet("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var authResult = await HttpContext.AuthenticateAsync();

            if (authResult?.Succeeded != true)
            {
                return Unauthorized("Authentication failed.");
            }

            var claims = authResult.Principal.Claims.ToList();
            var token = GenerateJwtToken(claims);
            return Ok(new { token });
        }

        // Generate JWT token
        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Jwt:Issuer"],
                audience: _configuration["Authentication:Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var user = HttpContext.User;
            return Ok(new
            {
                UserId = user.FindFirstValue(ClaimTypes.NameIdentifier),
                Name = user.FindFirstValue(ClaimTypes.Name),
                Email = user.FindFirstValue(ClaimTypes.Email)
            });
        }
    }
}
