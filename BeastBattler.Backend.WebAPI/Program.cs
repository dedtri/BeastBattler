using BeastBattler.Backend.Database;
using BeastBattler.Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BeastBattler.Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelopment", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            //Adding Connection
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<DatabaseContext>(options => options.EnableSensitiveDataLogging(false).UseMySql(connection, ServerVersion.AutoDetect(connection)), 64);

            // Adding UnitOfWork
            builder.Services.AddScoped<UnitOfWork>();

            // Adding Repositories
            builder.Services.AddScoped<CreatureRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<CageRepository>();

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