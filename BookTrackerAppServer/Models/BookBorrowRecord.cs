namespace BookTrackerAppServer.Models
{
    public class BookBorrowRecord
    {
        public int Id { get; set; }
        public string Borrower  { get; set; }
        public string Book { get; set; }
        public int Sn { get; set; }
        public string BDate { get; set; }
        public string RDate { get; set; }
        public long Quantity { get; set; }
        public float BorrowCharge { get; set; }
    }
}
