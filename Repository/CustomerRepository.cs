using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<Customers> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return customer;
        }

        public async Task AddCustomerAsync(CustomersDto model)
        {

                var NewCustomer = new Customers
                {
                    CustomerName = model.CustomerName,
                    Phone = model.Phone
                };

                await _context.Customers.AddAsync(NewCustomer);
                await _context.SaveChangesAsync();

        }

        public async Task UpdateCustomer(Customers model)
        {
            var customerFound = await _context.Customers.FindAsync(model.Id);

            if (customerFound != null)
            {
                customerFound.CustomerName = model.CustomerName;
                customerFound.Phone = model.Phone;

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

        public bool CheckIfCustomerExists(string CustomerName, int phonenumber)
        {
            var Customer = _context.Customers.Where(x => x.CustomerName == CustomerName && x.Phone == phonenumber).FirstOrDefault();

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
