using TravelTime.Backend.Database;
using TravelTime.Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TravelTime.Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelopment", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            //Adding Connection
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<DatabaseContext>(options =>
               options.EnableSensitiveDataLogging(false)
                      .UseSqlServer(connection), 64);

            // Adding UnitOfWork
            builder.Services.AddScoped<UnitOfWork>();

            // Adding Repositories
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<TripRepository>();

            //Adding Services

            // Adding MVC
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            // Adding Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enables Cors
            app.UseCors("AllowDevelopment");

            // Enable Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheGeneralStore.Backend.WebAPI V1");
                });
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}