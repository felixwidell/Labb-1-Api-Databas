using Microsoft.EntityFrameworkCore;
using MovieApp.Models;
using System;

namespace MovieApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Menu> Menus{ get; set; }
        public DbSet<Tables> Tables { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
