using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Managers;
using Restaurant.EF.Repositories;
using Restaurant.API.Middleware;


namespace Restaurant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Eindopdracht_Web4;Integrated Security=True;TrustServerCertificate=true";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IGebruikerRepo>(r => new RepoGebruikerEF(connectionString));
            builder.Services.AddSingleton<GebruikerManager>();
            builder.Services.AddSingleton<IReservatieRepo>(r => new RepoReservatiesEF(connectionString));
            builder.Services.AddSingleton<ReservatieManager>();
            builder.Services.AddSingleton<IRestaurantRepo>(r => new RepoRestaurantEF(connectionString));
            builder.Services.AddSingleton<RestaurantManager>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Services.AddLogging(); // Add this line to register ILogger

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = logFactory.CreateLogger("middleware");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRequestResponseLoggingMiddleware();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}