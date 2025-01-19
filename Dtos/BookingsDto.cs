using MovieApp.Models;

namespace MovieApp.Dtos
{
    public class BookingsDto
    {
        public int Customer { get; set; }
        public int TableId { get; set; }
        public DateTime BookingDate { get; set; }
        public int PeopleAmount { get; set; }


    }
}
