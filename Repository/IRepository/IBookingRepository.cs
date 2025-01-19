using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Bookings>> GetAllBookingAsync();
        Task<Bookings> GetBookingById(int id);
        Task AddBookingAsync(int CustomerId, int TableId, DateTime Date, int peopleAmount);
        bool CheckIfAvailable(DateTime date, int tableId);
        bool CheckIfAvailableUpdateBooking(DateTime date, int tableId);
        Task UpdateBookingAsync(BookingsDto bookingDto);
        Task DeleteBooking(int bookingId);
    }
}
