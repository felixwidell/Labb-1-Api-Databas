
using Microsoft.EntityFrameworkCore;
using MovieApp.Controllers;
using MovieApp.Data;
using MovieApp.Dtos;
using System;
using MovieApp.Repository;
using MovieApp.Repository.IRepository;

namespace RestaurantApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString("ApiContext");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            var app = builder.Build();

         

            //app.MapGet("/GetAllTables", async (DataContext db) =>
            //{
            //    var users = await db.Tables.ToListAsync();
            //    return Results.Ok(users);
            //});

            //app.MapGet("/GetAllBookings", async (DataContext db) =>
            //{
            //    var users = await db.Bookings.ToListAsync();
            //    return Results.Ok(users);
            //});

            //app.MapPost("/AddBooking/{customerId}/{tableId}/{peopleAmount}/{date}", BookingController.AddBooking);
            //app.MapPost("/ChangeBooking/{customerId}/{date}", BookingController.ChangeBooking);

            //app.MapGet("/GetAllCustomers", async (DataContext db) =>
            //{
            //    var users = await db.Customers.ToListAsync();
            //    return Results.Ok(users);
            //});

            //app.MapGet("/GetMenu", async (DataContext db) =>
            //{
            //    var users = await db.Menus.ToListAsync();
            //    return Results.Ok(users);
            //});

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }


}
