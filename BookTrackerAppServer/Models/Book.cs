namespace BookTrackerAppServer.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Sn { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<BookBorrowRecord> BookBorrowRecords { get; set; }
    }
}
