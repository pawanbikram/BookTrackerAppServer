using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookTrackerAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly BookDbContext _bookDbContext;
        private readonly IConfiguration _configuration;
        public AuthenticationController(BookDbContext bookDbContext, IConfiguration configuration)
        {
            _bookDbContext = bookDbContext;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Credential credentialLoginRequest)
        {
            var user = AuthenticateUser(credentialLoginRequest);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound();
        }
        private Credential AuthenticateUser(Credential credential)
        {
            var currentUser = _bookDbContext.credentials.FirstOrDefault(cu => cu.Username == credential.Username && cu.Password == credential.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
        private string GenerateToken(Credential credential)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, credential.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, credential.Username)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Credential credentialAddRequest)
        {
            await _bookDbContext.credentials.AddAsync(credentialAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(credentialAddRequest);
        }
    }
}
