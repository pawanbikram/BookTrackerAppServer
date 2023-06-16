namespace BookTrackerAppServer.Models
{
    public class BookBorrowRecord
    {
        public int Id { get; set; }
        public int BorrowerId  { get; set; }
        public int BookId { get; set; }
        public int Sn { get; set; }
        public string BDate { get; set; }
        public string RDate { get; set; }
        public long Quantity { get; set; }
        public long BorrowCharge { get; set; }
        public virtual Consumer Consumer { get; set; }
        public virtual Book Book { get; set; }
    }
}
