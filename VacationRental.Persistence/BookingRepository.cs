using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Domain.Booking;
using VacationRental.Domain.DTO.Booking;

namespace VacationRental.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDictionary<int, BookingViewModel> bookings;

        public BookingRepository(IDictionary<int, BookingViewModel> bookings)
        {
            this.bookings = bookings;
        }

        public bool Exists(int rentalId)
        {
            return this.bookings.ContainsKey(rentalId);
        }

        public BookingViewModel GetById(int rentalId)
        {
            return bookings[rentalId];
        }

        public List<BookingViewModel> GetBookingsPerRentalId(int rentalId)
        {
            return bookings.Values.Where(b => b.RentalId == rentalId).ToList();
        }

        public List<BookingViewModel> GetBookingsPerDayAndRentalId(int rentalId, DateTime date)
        {
            return bookings.Values.Where(b => b.RentalId == rentalId && b.Start <= date && b.Start.AddDays(b.Nights) > date).ToList();
        }

        public List<BookingViewModel> GetPreparationTimesPerDayAndRentalId(int rentalId, DateTime date, int preparationDays)
        {
            return bookings.Values.Where(b => b.RentalId == rentalId && b.Start.AddDays(b.Nights) <= date && b.Start.AddDays(b.Nights + preparationDays) > date).ToList();
        }

        public List<BookingViewModel> GetByRentalId(int rentalId)
        {
            return this.bookings.Where(b => b.Value.RentalId == rentalId).Select(b => b.Value).ToList();
        }

        public void Add(BookingViewModel model)
        {
            this.bookings.Add(model.Id, model);
        }
        
        public int GetNextId()
        {
            return bookings.Count + 1;
        }

        public DateTime GetFirstOcupacionDate(int rentalId)
        {
            return this.GetBookingsPerRentalId(rentalId).Min(b => b.Start);
        }

        public DateTime GetLastOcupationDate(int rentalId, int preparationTimeInDays = 0)
        {
             return this.GetBookingsPerRentalId(rentalId).Max(b => b.Start.AddDays(b.Nights + preparationTimeInDays));
        }
    }
}
