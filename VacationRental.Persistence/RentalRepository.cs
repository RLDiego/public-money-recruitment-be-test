using System.Collections.Generic;
using VacationRental.Domain.DTO.Rental;
using VacationRental.Domain.Rentals;

namespace VacationRental.Persistence
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, RentalViewModel> rentals;

        public RentalRepository(IDictionary<int, RentalViewModel> rentals)
        {
            this.rentals = rentals;
        }

        public void Add(RentalViewModel newRental)
        {
            rentals.Add(newRental.Id, newRental);
        }

        public bool Exists(int rentalId)
        {
            return this.rentals.ContainsKey(rentalId);
        }

        public RentalViewModel GetById(int rentalId)
        {
            return rentals[rentalId];
        }
        public int GetPreparationDays(int rentalId)
        {
            return this.GetById(rentalId).PreparationTimeInDays;
        }

        public int GetNextId()
        {
            return rentals.Count + 1;
        }

        public void Modify(RentalViewModel rental)
        {
            this.rentals[rental.Id] = rental;
        }
    }
}
