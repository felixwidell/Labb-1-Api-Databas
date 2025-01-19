using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task AddCustomerAsync(CustomersDto model);
        Task<IEnumerable<Customers>> GetAllCustomersAsync();
        Task<Customers> GetCustomerById(int id);
        Task UpdateCustomer(Customers model);
        Task DeleteCustomer(int customerId);
        bool CheckIfCustomerExists(string CustomerName, int phonenumber);
    }
}
