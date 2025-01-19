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

        public async Task<Menu> GetMenuById(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            return menu;
        }

        public async Task AddMenuAsync(MenuDto model)
        {

            var NewMenu = new Menu
            {
                FoodName = model.FoodName,
                Price = model.Price,
                IsAvaiable = model.IsAvaiable
            };

            await _context.Menus.AddAsync(NewMenu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuAsync(Menu model)
        {
            var MenuFound = _context.Menus.Where(x => x.Id == model.Id).FirstOrDefault();

            if(MenuFound != null)
            {
                MenuFound.FoodName = model.FoodName;
                MenuFound.Price = model.Price;
                MenuFound.IsAvaiable = model.IsAvaiable;
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
