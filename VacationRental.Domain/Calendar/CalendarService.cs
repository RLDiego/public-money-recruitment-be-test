using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Domain.Booking;
using VacationRental.Domain.DTO.Booking;
using VacationRental.Domain.DTO.Calendar;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Calendar
{
    public class CalendarService : ICalendarService
    {
        private IRentalRepository rentalRepository;
        private IBookingRepository bookingRepository;
        public CalendarService(IRentalRepository rentalRepository, IBookingRepository bookingRepository)
        {
            this.rentalRepository = rentalRepository;
            this.bookingRepository = bookingRepository;
        }

        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!this.rentalRepository.Exists(rentalId))
                throw new ApplicationException("Rental not found");            

            var rentalPreparationDays = rentalRepository.GetPreparationDays(rentalId);

            List<CalendarDateViewModel> bookings = GetBookingsAndPreparationTimesPerDay(rentalId, start, nights, rentalPreparationDays);

            return new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = bookings,
            };
        }

        private List<CalendarDateViewModel> GetBookingsAndPreparationTimesPerDay(int rentalId, DateTime start, int nights, int rentalPreparationDays)
        {
            var bookings = new List<CalendarDateViewModel>();

            for (var i = 0; i < nights; i++)
            {
                var date = start.Date.AddDays(i);
                var bookingUnits = this.bookingRepository.GetBookingsPerDayAndRentalId(rentalId, date).Select(b => new CalendarBookingViewModel() { Id = b.Id }).ToList();
                var preparationTimes = this.bookingRepository.GetPreparationTimesPerDayAndRentalId(rentalId, date, rentalPreparationDays).Select(b => new PreparationTime() { }).ToList();

                for (var y = 0; y <= bookingUnits.Count - 1; y++)
                {
                    bookingUnits[y].Unit = y + 1;
                }

                for (var y = 0; y <= preparationTimes.Count - 1; y++)
                {
                    preparationTimes[y].Unit = bookingUnits.Count + y + 1;
                }

                bookings.Add(new CalendarDateViewModel
                {
                    Date = date,
                    Bookings = bookingUnits,
                    PreparationTimes = preparationTimes
                });
            }

            return bookings;
        }
    }
}
