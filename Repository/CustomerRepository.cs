using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository.IRepository;

namespace MovieApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddCustomerAsync(string CustomerName, int PhoneNumber)
        {

                var NewCustomer = new Customers
                {
                    CustomerName = CustomerName,
                    Phone = PhoneNumber
                };

                await _context.Customers.AddAsync(NewCustomer);
                await _context.SaveChangesAsync();

        }

        public async Task UpdateCustomer(CustomersDto dto , int customerId)
        {
            var customerFound = await _context.Customers.FindAsync(customerId);

            if (customerFound != null)
            {
                customerFound.CustomerName = dto.CustomerName;
                customerFound.Phone = dto.Phone;

                _context.Customers.Update(customerFound);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customerFound = await _context.Customers.FindAsync(customerId);

            if (customerFound != null)
            {
                _context.Customers.Remove(customerFound);
                await _context.SaveChangesAsync();
            }
        }

        public bool CheckIfCustomerExists(string CustomerName)
        {
            var Customer = _context.Customers.Where(x => x.CustomerName == CustomerName).FirstOrDefault();

            if(Customer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
