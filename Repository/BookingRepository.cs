using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository.IRepository;

namespace MovieApp.Repository
{
    public class BookingRepository : IBookingRepository
    {
        DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bookings>> GetAllBookingAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task AddBookingAsync(int CustomerId, int TableId, DateTime Date, int peopleAmount)
        {
            
                var customer = await _context.Customers.FindAsync(CustomerId);
                var table = await _context.Tables.FindAsync(TableId);
                var NewBooking = new Bookings
                {
                    Customers = customer,
                    Tables = table,
                    BookingDate = Date,
                    PeopleAmount = peopleAmount
                };

                await _context.Bookings.AddAsync(NewBooking);
                await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(BookingsDto bookingDto, int customerId)
        {
            var bookingFound = _context.Bookings.Where(x => x.Customers.Id == customerId).FirstOrDefault();
            var table = _context.Tables.Where(x => x.Id == bookingDto.TableId).FirstOrDefault();

            if (bookingFound != null && table != null)
            {
                bookingFound.BookingDate = bookingDto.BookingDate;
                bookingFound.PeopleAmount = bookingDto.PeopleAmount;
                bookingFound.Tables = table;

                _context.Update(bookingFound);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task DeleteBooking(int bookingId)
        {
            var foundBooking = await _context.Bookings.FindAsync(bookingId);

            if(foundBooking != null)
            {
                _context.Remove(foundBooking);
                await _context.SaveChangesAsync();
            }

        }

        public bool CheckIfAvailable(DateTime date, int tableId)
        {
            bool DateAvailable = true;

            var BookingsList = _context.Bookings.Where(x => x.Tables.Id == tableId).ToList();

            foreach (var item in BookingsList)
            {
                TimeSpan difference = date - item.BookingDate;

                if (Math.Abs(difference.TotalHours) < 1)
                {
                    DateAvailable = false;
                }
            }

            return DateAvailable;
        }


    }
}
