using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowDetailController : Controller
    {
        private readonly BookDbContext _bookDbContext;
        public BorrowDetailController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBorrowDetails()
        {
            var borrowDetails = await _bookDbContext.borrowDetails.ToListAsync();
            return Ok(borrowDetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddBorrowDetail([FromBody] BorrowDetail borrowDetailAddRequest)
        {
            await _bookDbContext.borrowDetails.AddAsync(borrowDetailAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(borrowDetailAddRequest);
        }
    }
}
