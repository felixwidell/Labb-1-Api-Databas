using Microsoft.AspNetCore.Mvc;
using MovieApp.Repository.IRepository;
using MovieApp.Dtos;
using MovieApp.Models;

namespace MovieApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepo;

        public MenuController(IMenuRepository repository)
        {
            _menuRepo = repository;
        }

        [HttpGet]
        [Route("GetMenu")]
        public async Task<IActionResult> GetMenu()
        {
            var MenuList = await _menuRepo.GetMenu();
            return Ok(MenuList);
        }

        [HttpPost]
        [Route("AddMenu")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> AddMenu(string foodName, int price, bool isAvailable)
        {

            await _menuRepo.AddMenuAsync(foodName, price, isAvailable);
            return Ok();


        }

        [HttpPatch]
        [Route("UpdateMenu")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> UpdateMenu(MenuDto menuDto, int menuId)
        {
            await _menuRepo.UpdateMenuAsync(menuDto, menuId);
            return Ok();

        }

        [HttpDelete]
        [Route("DeleteMenu")]
        public async Task<ActionResult> DeleteMenu(int menuId)
        {
            await _menuRepo.DeleteMenuAsync(menuId);
            return Ok();
        }
    }
}