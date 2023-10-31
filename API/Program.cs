using API.Models.User;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            bool useInMemoryDatabase =  builder.Configuration.GetValue<bool>("In_Memory");

            if (useInMemoryDatabase)
            {
                builder.Services.AddDbContext<UserContext>(opt =>
               opt.UseInMemoryDatabase(databaseName: "memoryDB"));
            }
            else
            {
                builder.Services.AddDbContext<UserContext>(opt =>
               opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); ;
            }
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserService, UserService>();
            var app = builder.Build();

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