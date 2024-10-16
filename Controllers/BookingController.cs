using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepo;

        public BookingController(IBookingRepository repository)
        {
            _bookingRepo = repository;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult> GetAllBookings()
        {
            var BookingList = await _bookingRepo.GetAllBookingAsync();
            return Ok(BookingList);
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> AddBooking(int CustomerId, int TableId, DateTime Date, int peopleAmount)
        {
            if(_bookingRepo.CheckIfAvailable(Date, TableId))
            {
                await _bookingRepo.AddBookingAsync(CustomerId, TableId, Date, peopleAmount);
                return Ok();
            }
            else
            {
                return BadRequest("Table is already booked at that time");
            }

        }

        [HttpPatch]
        [Route("UpdateBooking")]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> UpdateBooking(BookingsDto bookingdto, int customerId)
        {
            if (_bookingRepo.CheckIfAvailable(bookingdto.BookingDate, bookingdto.TableId))
            {
                await _bookingRepo.UpdateBookingAsync(bookingdto, customerId);
                return Ok(bookingdto);
            }
            else
            {
                return BadRequest("Table is already booked at that time");
            }
        }

        [HttpDelete]
        [Route("DeleteBooking")]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            await _bookingRepo.DeleteBooking(bookingId);
            return Ok();
        }
    }
}
