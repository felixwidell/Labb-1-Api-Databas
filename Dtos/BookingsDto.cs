using MovieApp.Models;

namespace MovieApp.Dtos
{
    public class BookingsDto
    {
        public int TableId { get; set; }
        public DateTime BookingDate { get; set; }
        public int PeopleAmount { get; set; }
    }
}
