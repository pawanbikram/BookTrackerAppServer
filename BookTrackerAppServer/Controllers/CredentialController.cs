using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CredentialController : Controller
    {
        private readonly BookDbContext _bookDbContext;
        public CredentialController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCredentials()
        {
            var credentials = await _bookDbContext.credentials.ToListAsync();
            return Ok(credentials);
        }
        [HttpPost]
        public async Task<IActionResult> AddCredential([FromBody] Credential credentialAddRequest)
        {
            await _bookDbContext.credentials.AddAsync(credentialAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(credentialAddRequest);
        }
    }
}
