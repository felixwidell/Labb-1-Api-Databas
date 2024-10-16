using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Dtos;
using MovieApp.Models;
using MovieApp.Repository;
using MovieApp.Repository.IRepository;

namespace MovieApp.Repository
{
    public class TableRepository : ITableRepository
    {
        DataContext _context;

        public TableRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Tables>> GetAllTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Tables> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            return table;
        }

        public async Task<Tables> AddTable(int Seats)
        {
            var newTable = new Tables
            {
                Seats = Seats
            };
            await _context.AddAsync(newTable);
            await _context.SaveChangesAsync();
            return newTable;
        }

        public async Task<Tables> UpdateTable(int newSeats, int tableId)
        {
            var tableFound = await _context.Tables.FindAsync(tableId);

            if (tableFound != null)
            {
                tableFound.Seats = newSeats;
                _context.Update(tableFound);
                await _context.SaveChangesAsync();
                return tableFound;
            }
            else
            {
                return tableFound;
            }
        }

        public async Task DeleteTable(int tableId)
        {
            var tableFound = await _context.Tables.FindAsync(tableId);

            if (tableFound != null)
            {
                _context.Remove(tableFound);
                await _context.SaveChangesAsync();
            }
        }
    }


}
