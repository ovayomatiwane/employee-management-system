
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace WebApi
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

            builder.Services.AddDbContext<EmployeeManagementDataContext>(options => 
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("EmployeeManagementDataContext"))
            );

            //Add controller-dependent services here
            builder.Services.AddScoped<IConsultantsService, ConsultantsService>();
            builder.Services.AddScoped<IConsultantTasksService, ConsultantTasksService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<ITasksService, TasksService>();


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
