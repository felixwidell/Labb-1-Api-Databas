using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Dtos;
using MovieApp.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepo;
        
        public TableController(ITableRepository repository)
        {
            _tableRepo = repository;
        }

        [HttpGet]
        [Route("tables/GetAllTables")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables()
        {
            var tableList = await _tableRepo.GetAllTablesAsync();
            return Ok(tableList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTableById(int id)
        {
            var table = await _tableRepo.GetTableByIdAsync(id);
            return Ok(table);
        }

        [HttpPost]
        [Route("AddTable")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddTable(TableDto model)
        {
             var newTable = await _tableRepo.AddTable(model.Seats);
            return Ok(newTable);
        }

        [HttpPatch]
        [Route("UpdateTable")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateTable(Tables model)
        {
            var updatedTable = await _tableRepo.UpdateTable(model.Seats, model.Id);
            return Ok(updatedTable);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            await _tableRepo.DeleteTable(id);
            return Ok();
        }
    }
}
