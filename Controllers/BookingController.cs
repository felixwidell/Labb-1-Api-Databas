using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("GetAllBookings")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetAllBookings()
        {
            var BookingList = await _bookingRepo.GetAllBookingAsync();
            return Ok(BookingList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetBookingById(int id)
        {
            var Booking = await _bookingRepo.GetBookingById(id);
            return Ok(Booking);
        }

        [HttpPost]
        [Route("AddBooking")]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> AddBooking(Bookings model)
        {
            if(_bookingRepo.CheckIfAvailable(model.BookingDate, model.Tables.Id))
            {
                await _bookingRepo.AddBookingAsync(model.Customers.Id, model.Tables.Id, model.BookingDate, model.PeopleAmount);
                return Ok();
            }
            else
            {
                return BadRequest("Table is already booked at that time");
            }

        }

        [HttpPatch]
        [Route("UpdateBooking")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<BookingsDto>>> UpdateBooking(BookingsDto bookingdto)
        {
            if (_bookingRepo.CheckIfAvailableUpdateBooking(bookingdto.BookingDate, bookingdto.TableId))
            {
                await _bookingRepo.UpdateBookingAsync(bookingdto);
                return Ok(bookingdto);
            }
            else
            {
                return BadRequest("Table is already booked at that time");
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            await _bookingRepo.DeleteBooking(id);
            return Ok();
        }
    }
}
