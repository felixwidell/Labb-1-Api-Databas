using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Repository.IRepository;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository repository)
        {
            _customerRepo = repository;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> GetAllCustomers()
        {
            var tableList = await _customerRepo.GetAllCustomersAsync();
            return Ok(tableList);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> AddCustomer(string CustomerName, int PhoneNumber)
        {
            if(_customerRepo.CheckIfCustomerExists(CustomerName))
            {
                return BadRequest("Customer already exists");
            }
            else
            {
                await _customerRepo.AddCustomerAsync(CustomerName, PhoneNumber);
                return Ok();
            }
        }

        [HttpPatch]
        [Route("UpdateCustomer")]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> UpdateCustomer(CustomersDto dto, int customerId)
        {
            await _customerRepo.UpdateCustomer(dto, customerId);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            await _customerRepo.DeleteCustomer(customerId);
            return Ok();
        }
    }
}
