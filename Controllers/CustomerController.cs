using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Dtos;
using MovieApp.Models;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomersDto>> GetCustomerById(int id)
        {
            var customer = await _customerRepo.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> AddCustomer(CustomersDto model)
        {
            if(_customerRepo.CheckIfCustomerExists(model.CustomerName, model.Phone))
            {
                return BadRequest("Customer already exists");
            }
            else
            {
                await _customerRepo.AddCustomerAsync(model);
                return Ok();
            }
        }

        [HttpPatch]
        [Route("UpdateCustomer")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<CustomersDto>>> UpdateCustomer(Customers model)
        {
            await _customerRepo.UpdateCustomer(model);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerRepo.DeleteCustomer(id);
            return Ok();
        }
    }
}
