using VacationRental.Domain.DTO.Common;
using VacationRental.Domain.DTO.Rental;

namespace VacationRental.Domain.Rentals
{
    public interface IRentalService
    {
        RentalViewModel GetById(int rentalId);
        ResourceIdViewModel Add(RentalBindingModel model);
        void Modify(int rentalId, RentalBindingModel model);
    }
}
