using System;
using System.Collections.Generic;
using VacationRental.Domain.DTO.Booking;

namespace VacationRental.Domain.Booking
{
    public interface IBookingRepository
    {
        bool Exists(int rentalId);
        BookingViewModel GetById(int rentalId);
        List<BookingViewModel> GetBookingsPerRentalId(int rentalId);
        List<BookingViewModel> GetBookingsPerDayAndRentalId(int rentalId, DateTime date);
        List<BookingViewModel> GetPreparationTimesPerDayAndRentalId(int rentalId, DateTime date, int preparationDays);
        List<BookingViewModel> GetByRentalId(int rentalId);
        void Add(BookingViewModel model);
        int GetNextId();
        DateTime GetFirstOcupacionDate(int rentalId);
        DateTime GetLastOcupationDate(int rentalId, int preparationTimeInDays = 0);
    }
}
