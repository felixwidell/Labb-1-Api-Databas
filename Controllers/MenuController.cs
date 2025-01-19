using Microsoft.AspNetCore.Mvc;
using MovieApp.Repository.IRepository;
using MovieApp.Dtos;
using MovieApp.Models;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<MenuDto>> GetMenuById(int id)
        {
            var menu = await _menuRepo.GetMenuById(id);
            return Ok(menu);
        }

        [HttpPost]
        [Route("AddMenu")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> AddMenu(MenuDto model)
        {

            await _menuRepo.AddMenuAsync(model);
            return Ok();


        }

        [HttpPatch]
        [Route("UpdateMenu")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<MenuDto>>> UpdateMenu(Menu model)
        {
            await _menuRepo.UpdateMenuAsync(model);
            return Ok();

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeleteMenu(int id)
        {
            await _menuRepo.DeleteMenuAsync(id);
            return Ok();
        }
    }
}