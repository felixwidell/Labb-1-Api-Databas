using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository.IRepository;

namespace MovieApp.Repository
{
    public class MenuRepository : IMenuRepository
    {
        DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetMenu()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task AddMenuAsync(string foodName, int price, bool isAvailable)
        {

            var NewMenu = new Menu
            {
                FoodName = foodName,
                Price = price,
                IsAvaiable = isAvailable
            };

            await _context.Menus.AddAsync(NewMenu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuAsync(MenuDto menuDto, int MenuId)
        {
            var MenuFound = _context.Menus.Where(x => x.Id == MenuId).FirstOrDefault();

            if(MenuFound != null)
            {
                MenuFound.FoodName = menuDto.FoodName;
                MenuFound.Price = menuDto.Price;
                MenuFound.IsAvaiable = menuDto.IsAvaiable;
                _context.Update(MenuFound);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteMenuAsync(int menuId)
        {
            var MenuFound = await _context.Menus.FindAsync(menuId);
            if(MenuFound != null)
            {
                _context.Menus.Remove(MenuFound);
                await _context.SaveChangesAsync();
            }
        }
    }
}
