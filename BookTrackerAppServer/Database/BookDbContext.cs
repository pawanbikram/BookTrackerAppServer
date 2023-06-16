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
        public DbSet<BorrowDetail> borrowDetails { get; set; }
        public DbSet<BookBorrowRecord> bookBorrowRecords { get; set; }
        public DbSet<Contact> contacts { get; set; }       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBorrowRecord>().HasOne(c => c.Book).WithMany(p => p.BookBorrowRecords).HasForeignKey(c => c.BookId);
            modelBuilder.Entity<BookBorrowRecord>().HasOne(c => c.Consumer).WithMany(p => p.BookBorrowRecords).HasForeignKey(c => c.BorrowerId);
        }
    }
}
