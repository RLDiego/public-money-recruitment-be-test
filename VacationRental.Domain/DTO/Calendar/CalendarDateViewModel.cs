using System;
using System.Collections.Generic;

namespace VacationRental.Domain.DTO.Calendar
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        public List<PreparationTime> PreparationTimes { get; set; }
    }
    public class PreparationTime
    {
        public int Unit { get; set; }
    }
}
