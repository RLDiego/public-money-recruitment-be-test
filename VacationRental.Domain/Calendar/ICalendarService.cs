using System;
using VacationRental.Domain.DTO.Calendar;

namespace VacationRental.Domain.Calendar
{
    public interface ICalendarService
    {
        CalendarViewModel Get(int rentalId, DateTime start, int nights);
    }
}
