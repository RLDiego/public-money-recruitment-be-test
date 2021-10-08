using VacationRental.Domain.DTO.Booking;
using VacationRental.Domain.DTO.Common;

namespace VacationRental.Domain.Booking
{
    public interface IBookingService
    {
        BookingViewModel Get(int bookingId);
        ResourceIdViewModel Add(BookingBindingModel model);
    }
}
