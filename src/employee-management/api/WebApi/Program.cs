
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Auth;
using WebApi.Auth.Services.Interfaces;
using WebApi.Auth.Services;
using WebApi.Data;
using WebApi.Services;
using WebApi.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Bind the Jwt config
            builder.Services.Configure<JwtOptions>(
                builder.Configuration.GetSection("Jwt"));
            var jwtOptions = builder.Configuration
                                    .GetSection("Jwt")
                                    .Get<JwtOptions>()!;

            builder.Services
                   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = jwtOptions.Issuer,
                           ValidAudience = jwtOptions.Audience,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                       };
                   });

            builder.Services.AddAuthorization();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<EmployeeManagementDataContext>(options => 
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("EmployeeManagementDataContext"))
            );

            //Add controller-dependent services here
            builder.Services.AddScoped<IConsultantsService, ConsultantsService>();
            builder.Services.AddScoped<IConsultantTasksService, ConsultantTasksService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<ITasksService, TasksService>();
            builder.Services.AddScoped<IRoleRatesService, RoleRatesService>();
            builder.Services.AddScoped<IBusinessRulesService, BusinessRulesService>();
            builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
            builder.Services.AddScoped<IImagesService, ImagesService>();

            //Auth
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Employee Management",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
