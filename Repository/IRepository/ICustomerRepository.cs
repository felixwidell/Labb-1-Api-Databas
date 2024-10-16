using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(string CustomerName, int PhoneNumber);
        Task<IEnumerable<Customers>> GetAllCustomersAsync();
        Task UpdateCustomer(CustomersDto dto, int customerId);
        Task DeleteCustomer(int customerId);
        bool CheckIfCustomerExists(string CustomerName);
    }
}
