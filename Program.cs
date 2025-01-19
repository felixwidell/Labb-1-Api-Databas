
using Microsoft.EntityFrameworkCore;
using MovieApp.Controllers;
using MovieApp.Data;
using MovieApp.Dtos;
using System;
using MovieApp.Repository;
using MovieApp.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters 
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Cookies skickas endast över HTTPS
                    options.Cookie.SameSite = SameSiteMode.None; // Tillåt cross-site cookies
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            string connectionString = builder.Configuration.GetConnectionString("ApiContext");
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", builder =>
                {
                    builder.AllowAnyOrigin() // Lägg till React-appens URL
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");




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
