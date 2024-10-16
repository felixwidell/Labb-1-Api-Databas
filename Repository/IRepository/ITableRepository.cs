using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface ITableRepository
    {
        Task<IEnumerable<Tables>> GetAllTablesAsync();
        Task<Tables> GetTableByIdAsync(int tableId);
        Task<Tables> AddTable(int Seats);
        Task<Tables> UpdateTable(int newSeats, int tableId);
        Task DeleteTable(int tableId);
    }
}
