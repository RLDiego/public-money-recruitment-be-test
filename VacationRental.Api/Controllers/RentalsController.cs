using Microsoft.AspNetCore.Mvc;
using System;
using VacationRental.Domain.DTO.Rental;
using VacationRental.Domain.Rentals;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService rentalService;

        public RentalsController(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public IActionResult Get(int rentalId)
        {
            try
            {
                return Ok(this.rentalService.GetById(rentalId));
            }
            catch (ApplicationException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post(RentalBindingModel model)
        {
            try
            {
                return Ok(this.rentalService.Add(model));
            }
            catch (ApplicationException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public IActionResult Put(int rentalId, RentalBindingModel model)
        {
            try
            {
                this.rentalService.Modify(rentalId, model);
                return Ok();
            }
            catch (ApplicationException)
            {
                return BadRequest();
            }
        }
    }
}
