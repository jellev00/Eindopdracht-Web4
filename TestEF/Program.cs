using Restaurant.EF.Models;

namespace TestEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EF Model Test");

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Eindopdracht_Web4;Integrated Security=True;TrustServerCertificate=true";

            RestaurantContext ctx = new RestaurantContext(connectionString);

            ctx.Database.EnsureDeleted();
            Console.WriteLine("DB Deleted");
            ctx.Database.EnsureCreated();
            Console.WriteLine("DB Created");
        }
    }
}