namespace BookTrackerAppServer.Models
{
    public class BorrowDetail
    {
        public int Id { get; set; }
        public int Sn { get; set; }
        public string Name { get; set; }
        public string Book { get; set; }
        public long Quantity { get; set; }
        public string Bdate { get; set; }
        public string Rdate { get; set; }
    }
}