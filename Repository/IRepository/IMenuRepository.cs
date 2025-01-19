using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenu();
        Task<Menu> GetMenuById(int id);
        Task AddMenuAsync(MenuDto model);
        Task UpdateMenuAsync(Menu model);
        Task DeleteMenuAsync(int menuId);
    }
}
