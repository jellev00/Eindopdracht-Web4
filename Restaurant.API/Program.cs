using Restaurant.BL.Interfaces;
using Restaurant.BL.Managers;
using Restaurant.EF.Repositories;

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


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}