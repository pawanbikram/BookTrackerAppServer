using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]    
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BookDbContext _bookDbContext;
        public ContactController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _bookDbContext.contacts.ToListAsync();
            return Ok(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] Contact contactAddRequest)
        {
            await _bookDbContext.contacts.AddAsync(contactAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(contactAddRequest);
        }
    }
}
