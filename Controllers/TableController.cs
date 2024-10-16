using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Dtos;
using MovieApp.Repository.IRepository;

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
        [Route("tables/GetTableById")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTableById(int tableId)
        {
            var table = await _tableRepo.GetTableByIdAsync(tableId);
            return Ok(table);
        }

        [HttpPost]
        [Route("AddTable")]
        public async Task<IActionResult> AddTable(int seats)
        {
             var newTable = await _tableRepo.AddTable(seats);
            return Ok(newTable);
        }

        [HttpPatch]
        [Route("UpdateTable")] 
        public async Task<IActionResult> UpdateTable(int newSeats, int tableId)
        {
            var updatedTable = await _tableRepo.UpdateTable(newSeats, tableId);
            return Ok(updatedTable);
        }

        [HttpDelete]
        [Route("DeleteTable")]
        public async Task<IActionResult> DeleteTable(int tableId)
        {
            await _tableRepo.DeleteTable(tableId);
            return Ok();
        }
    }
}
