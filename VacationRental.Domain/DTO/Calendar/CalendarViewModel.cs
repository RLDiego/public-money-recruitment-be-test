using System.Collections.Generic;

namespace VacationRental.Domain.DTO.Calendar
{ 
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
    }
}
