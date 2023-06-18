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
    public class BookController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly BookDbContext _bookDbContext;
        public BookController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookDbContext.books.ToListAsync();
            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book bookAddRequest)
        {
            await _bookDbContext.books.AddAsync(bookAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(bookAddRequest);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            var book = await _bookDbContext.books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, Book updateBookRequest)
        {
            var book = await _bookDbContext.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Sn = updateBookRequest.Sn;
            book.Name = updateBookRequest.Name;
            book.Author = updateBookRequest.Author;
            book.Price = updateBookRequest.Price;
            book.Quantity = updateBookRequest.Quantity;
            book.Status = updateBookRequest.Status;
            await _bookDbContext.SaveChangesAsync();
            return Ok(book);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _bookDbContext.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookDbContext.books.Remove(book);
            await _bookDbContext.SaveChangesAsync();
            return Ok(book);
        }
    }
}
