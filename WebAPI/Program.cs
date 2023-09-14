using Microsoft.EntityFrameworkCore;
using WebAPI.DbContext;
using WebAPI.Controllers;
using System.Text.Json.Serialization;
using WebAPI.DbRepository.Interfaces;
using WebAPI.DbRepository.Implementations;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(
                opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
            builder.Services.AddDbContext<CompanyMSSqlDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddScoped<IEmployeesRepository, EmployeesRepositoryMSSQL>();
            builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepositoryMSSQL>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }

}