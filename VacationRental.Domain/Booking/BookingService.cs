using System;
using System.Linq;
using VacationRental.Domain.DTO.Booking;
using VacationRental.Domain.DTO.Common;
using VacationRental.Domain.Rentals;

namespace VacationRental.Domain.Booking
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IRentalRepository rentalRepository;

        public BookingService(IBookingRepository bookingRepository, IRentalRepository rentalRepository)
        {
            this.bookingRepository = bookingRepository;
            this.rentalRepository = rentalRepository;
        }

        public ResourceIdViewModel Add(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!this.rentalRepository.Exists(model.RentalId))
                throw new ApplicationException("Rental not found");

            var preparationDays = this.rentalRepository.GetPreparationDays(model.RentalId);
            var bookedTimeinDays = model.Nights + preparationDays;

            for (var i = 0; i < bookedTimeinDays; i++)
            {
                CheckAvaiability(model, preparationDays, bookedTimeinDays);
            }

            var key = new ResourceIdViewModel { Id = this.bookingRepository.GetNextId() };
            var newBooking = new BookingViewModel
            {
                Id = key.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start.Date
            };

            this.bookingRepository.Add(newBooking);

            return key;
        }       

        public BookingViewModel Get(int bookingId)
        {
            if (!this.bookingRepository.Exists(bookingId))
                throw new ApplicationException("Booking not found");

            return this.bookingRepository.GetById(bookingId);
        }

        private void CheckAvaiability(BookingBindingModel model, int preparationDays, int bookedTimeinDays)
        {
            var bookingsInRental = this.bookingRepository.GetByRentalId(model.RentalId);
            var count = bookingsInRental.Where(b => (b.Start <= model.Start.Date && b.Start.AddDays(b.Nights + preparationDays) > model.Start.Date)
                    || (b.Start < model.Start.AddDays(bookedTimeinDays) && b.Start.AddDays(b.Nights + preparationDays) >= model.Start.AddDays(bookedTimeinDays))
                    || (b.Start > model.Start && b.Start.AddDays(b.Nights + preparationDays) < model.Start.AddDays(bookedTimeinDays))).Count();
            
            if (count >= this.rentalRepository.GetById(model.RentalId).Units)
                throw new ApplicationException("Not available");
        }
    }
}
