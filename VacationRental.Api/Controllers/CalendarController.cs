using Microsoft.AspNetCore.Mvc;
using System;
using VacationRental.Domain.Calendar;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        [HttpGet]
        public IActionResult Get(int rentalId, DateTime start, int nights)
        {
            try
            {
                return Ok(this.calendarService.Get(rentalId, start, nights));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
