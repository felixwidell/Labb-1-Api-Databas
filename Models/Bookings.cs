namespace MovieApp.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Tables Tables { get; set; }
        public DateTime BookingDate { get; set; }
        public int PeopleAmount { get; set; }
    }
}
