using VacationRental.Domain.DTO.Rental;

namespace VacationRental.Domain.Rentals
{
    public interface IRentalRepository
    {
        bool Exists(int rentalId);
        RentalViewModel GetById(int rentalId);
        int GetNextId();
        void Add(RentalViewModel newRental);
        int GetPreparationDays(int rentalId);
        void Modify(RentalViewModel rental);
    }
}
