using BookTrackerAppServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Database
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Credential> credentials { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Consumer> consumers { get; set; }
        public DbSet<BookBorrowRecord> bookBorrowRecords { get; set; }
        public DbSet<Contact> contacts { get; set; }
    }
}
