using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Repository.IRepository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenu();
        Task AddMenuAsync(string foodName, int price, bool isAvailable);
        Task UpdateMenuAsync(MenuDto menuDto, int MenuId);
        Task DeleteMenuAsync(int menuId);
    }
}
