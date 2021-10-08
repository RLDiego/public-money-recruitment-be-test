using System;
using System.Linq;
using VacationRental.Domain.Booking;
using VacationRental.Domain.DTO.Common;
using VacationRental.Domain.DTO.Rental;

namespace VacationRental.Domain.Rentals
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository rentalRepository;
        private readonly IBookingRepository bookingRepository;

        public RentalService(IRentalRepository rentalRepository, IBookingRepository bookingRepository)
        {
            this.rentalRepository = rentalRepository;
            this.bookingRepository = bookingRepository;
        }

        public RentalViewModel GetById(int rentalId)
        {
            if (!this.rentalRepository.Exists(rentalId))
                throw new ApplicationException("Rental not found");

            return this.rentalRepository.GetById(rentalId);
        }

        public ResourceIdViewModel Add(RentalBindingModel model)
        {
            if (model.Units < 1)
                throw new ApplicationException("Rental units must be positive");
            if (model.PreparationTimeInDays < 0)
                throw new ApplicationException("The preparation time in days cannot be less than zero");

            var nextId = new ResourceIdViewModel { Id = this.rentalRepository.GetNextId() };
            var newRental = new RentalViewModel()
            {
                Id = nextId.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            };

            this.rentalRepository.Add(newRental);

            return nextId;
        }

        public void Modify(int rentalId, RentalBindingModel model)
        {
            if (!this.rentalRepository.Exists(rentalId))
                throw new ApplicationException("Rental not found");

            var rental = this.rentalRepository.GetById(rentalId);

            if (rental.Units >= model.Units && rental.PreparationTimeInDays >= model.PreparationTimeInDays)
            {
                CheckIfTheUpdateIsAllowed(rentalId, model);
            }

            rental.PreparationTimeInDays = model.PreparationTimeInDays;
            rental.Units = model.Units;

            this.rentalRepository.Modify(rental);
        }

        private void CheckIfTheUpdateIsAllowed(int rentalId, RentalBindingModel model)
        {
            var minDate = this.bookingRepository.GetFirstOcupacionDate(rentalId);
            var maxDate = this.bookingRepository.GetLastOcupationDate(rentalId, model.PreparationTimeInDays);
            var bookings = this.bookingRepository.GetBookingsPerRentalId(rentalId);
            for (var i = 0; i <= maxDate.Subtract(minDate).Days; i++)
            {
                if (bookings.Where(b => b.Start <= minDate.AddDays(i) && b.Start.AddDays(b.Nights + model.PreparationTimeInDays) >= minDate.AddDays(i)).Count() > model.Units)
                    throw new ApplicationException("The actual bookings do not allow this update");
            }
        }
    }
}
