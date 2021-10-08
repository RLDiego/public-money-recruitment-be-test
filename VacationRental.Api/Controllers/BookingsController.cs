using Microsoft.AspNetCore.Mvc;
using System;
using VacationRental.Domain.Booking;
using VacationRental.Domain.DTO.Booking;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService bookingService;

        public BookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public IActionResult Get(int bookingId)
        {
            try
            {
                return Ok(this.bookingService.Get(bookingId));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(BookingBindingModel model)
        {
            try
            {
                return Ok(this.bookingService.Add(model));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
