using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookBorrowRecordController : Controller
    {
        private readonly BookDbContext _bookDbContext;
        public BookBorrowRecordController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookBorrowRecords()
        {
            var bookBorrowRecords = await _bookDbContext.bookBorrowRecords.ToListAsync();
            return Ok(bookBorrowRecords);
        }
        [HttpPost]
        public async Task<IActionResult> AddBookBorrowRecord([FromBody] BookBorrowRecord bookBorrowRecordAddRequest)
        {
            await _bookDbContext.bookBorrowRecords.AddAsync(bookBorrowRecordAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(bookBorrowRecordAddRequest);
        }
    }
}
