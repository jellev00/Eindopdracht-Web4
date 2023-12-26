using Restaurant.BL.Interfaces;
using Restaurant.BL.Managers;
using Restaurant.BL.Models;
using Restaurant.EF.Models;
using Restaurant.EF.Repositories;
using System.Reflection.Metadata;

namespace TestEF
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EF Model Test");

            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Eindopdracht_Web4;Integrated Security=True;TrustServerCertificate=true";

            RestaurantContext ctx = new RestaurantContext(connectionString);

            IGebruikerRepo gebruikerRepo = new RepoGebruikerEF(connectionString);
            GebruikerManager gebruikerManager = new GebruikerManager(gebruikerRepo);

            IRestaurantRepo restaurantRepo = new RepoRestaurantEF(connectionString);
            RestaurantManager restaurantManager = new RestaurantManager(restaurantRepo);

            IReservatieRepo reservatieRepo = new RepoReservatiesEF(connectionString);
            ReservatieManager reservatieManager = new ReservatieManager(reservatieRepo);


            ctx.Database.EnsureDeleted();
            Console.WriteLine("DB Deleted");
            ctx.Database.EnsureCreated();
            Console.WriteLine("DB Created");

            Locatie locatie1 = new Locatie("9000", "Oudenaarde", "Nederstraat", "50");
            Contactgegevens contactgegevens1 = new Contactgegevens("0487472075", "pasta-piccaso@info.be");
            Restaurant.BL.Models.Restaurant restaurant1 = new Restaurant.BL.Models.Restaurant("Pasta Piccaso", locatie1, "italiaans", contactgegevens1, true);

            Locatie locatie2 = new Locatie("9770", "Kruisem", "Riemegemstraat", "1");
            Contactgegevens contactgegevens2 = new Contactgegevens("093835848", "Hof-van-Cleve@info.be");
            Restaurant.BL.Models.Restaurant restaurant2 = new Restaurant.BL.Models.Restaurant("Hof van Cleve", locatie2, "culinaire", contactgegevens2, true);

            restaurantManager.AddRestaurant(restaurant1);
            restaurantManager.AddRestaurant(restaurant2);

            var restaurant01 = restaurantManager.GetRestaurantByNaam("Pasta Piccaso");
            var restaurant02 = restaurantManager.GetRestaurantByNaam("Hof van Cleve");

            Tafel tafel1 = new Tafel(2, restaurant01);
            Tafel tafel2 = new Tafel(3, restaurant01);
            Tafel tafel3 = new Tafel(4, restaurant01);
            Tafel tafel4 = new Tafel(6, restaurant01);
            Tafel tafel5 = new Tafel(8, restaurant01);
            Tafel tafel6 = new Tafel(12, restaurant01);

            Tafel tafel01 = new Tafel(2, restaurant02);
            Tafel tafel02 = new Tafel(3, restaurant02);
            Tafel tafel03 = new Tafel(4, restaurant02);
            Tafel tafel04 = new Tafel(6, restaurant02);
            Tafel tafel05 = new Tafel(8, restaurant02);
            Tafel tafel06 = new Tafel(12, restaurant02);


            restaurantManager.AddTafel(restaurant01.Id, tafel1);
            restaurantManager.AddTafel(restaurant01.Id, tafel2);
            restaurantManager.AddTafel(restaurant01.Id, tafel3);
            restaurantManager.AddTafel(restaurant01.Id, tafel4);
            restaurantManager.AddTafel(restaurant01.Id, tafel5);
            restaurantManager.AddTafel(restaurant01.Id, tafel6);

            restaurantManager.AddTafel(restaurant02.Id, tafel01);
            restaurantManager.AddTafel(restaurant02.Id, tafel02);
            restaurantManager.AddTafel(restaurant02.Id, tafel03);
            restaurantManager.AddTafel(restaurant02.Id, tafel04);
            restaurantManager.AddTafel(restaurant02.Id, tafel05);
            restaurantManager.AddTafel(restaurant02.Id, tafel06);

            Console.WriteLine();

            var resto = restaurantManager.GetAllRestaurants();
            Console.WriteLine("GetAllRestaurants");
            foreach (Restaurant.BL.Models.Restaurant r in resto)
            {
                Console.WriteLine($"Restaurant: {r.Naam}, Keuken: {r.Keuken}, Locatie: {r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Gemeentenaam} {r.Locatie.Postcode}");
            }

            Console.WriteLine();

            var restoByNaam = restaurantManager.GetRestaurantByNaam("Hof van Cleve");
            Console.WriteLine("GetRestaurantByNaam");
            Console.WriteLine($"Restaurant: {restoByNaam.Naam}, Keuken: {restoByNaam.Keuken}, Locatie: {restoByNaam.Locatie.Straatnaam} {restoByNaam.Locatie.Huisnummerlabel}, {restoByNaam.Locatie.Gemeentenaam} {restoByNaam.Locatie.Postcode}");

            Console.WriteLine();

            var restoByPostcode = restaurantManager.SearchRestaurants("9000", null);
            Console.WriteLine("SearchRestaurants (Postcode)");
            foreach (Restaurant.BL.Models.Restaurant r in restoByPostcode)
            {
                Console.WriteLine($"Restaurant: {r.Naam}, Keuken: {r.Keuken}, Locatie: {r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Gemeentenaam} {r.Locatie.Postcode}");
            }

            Console.WriteLine();

            var restoByKeuken = restaurantManager.SearchRestaurants(null, "culinaire");
            Console.WriteLine("SearchRestaurants (Keuken)");
            foreach (Restaurant.BL.Models.Restaurant r in restoByKeuken)
            {
                Console.WriteLine($"Restaurant: {r.Naam}, Keuken: {r.Keuken}, Locatie: {r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Gemeentenaam} {r.Locatie.Postcode}");
            }

            Console.WriteLine();

            var restoByPostcodeKeuken = restaurantManager.SearchRestaurants("9000", "italiaans");
            Console.WriteLine("SearchRestaurants (Postcode & Keuken)");
            foreach (Restaurant.BL.Models.Restaurant r in restoByPostcodeKeuken)
            {
                Console.WriteLine($"Restaurant: {r.Naam}, Keuken: {r.Keuken}, Locatie: {r.Locatie.Straatnaam} {r.Locatie.Huisnummerlabel}, {r.Locatie.Gemeentenaam} {r.Locatie.Postcode}");
            }

            Console.WriteLine();

            var tafels01 = restaurantManager.GetTafels(1);
            Console.WriteLine("GetTafels (restaurantID)");
            foreach (Tafel t in tafels01)
            {
                Console.WriteLine($"Tafels: {t.Id}, Aantalplaatsen: {t.AantalPlaatsen}, Restaurant: {t.Restaurant.Naam}");
            }

            Console.WriteLine();

            var tafels02 = restaurantManager.GetTafels(2);
            Console.WriteLine("GetTafels (restaurantID)");
            foreach (Tafel t in tafels02)
            {
                Console.WriteLine($"Tafels: {t.Id}, Aantalplaatsen: {t.AantalPlaatsen}, Restaurant: {t.Restaurant.Naam}");
            }

            Locatie locatieG = new Locatie("9750", "Kruisem", "Toekomststraat", "16");
            Gebruiker gebruiker = new Gebruiker("Jelle", "jelle.vandriessche@gmail.com", "0472533243", locatieG);

            gebruikerManager.AddGebruiker(gebruiker);

            Console.WriteLine();

            var g = gebruikerManager.GetGebruikerByEmail("jelle.vandriessche@gmail.com");
            Console.WriteLine("Gebruiker");
            Console.WriteLine($"Gebruiker: {g.Naam}, Email: {g.Email}, TelefoonNr: {g.TelefoonNr}, Locatie: {g.Locatie.Straatnaam} {g.Locatie.Huisnummerlabel}, {g.Locatie.Gemeentenaam} {g.Locatie.Postcode}");

            DateTime reservationTime1 = new DateTime(2023, 12, 25, 18, 30, 0);
            DateTime reservationTime2 = new DateTime(2023, 12, 30, 18, 30, 0);

            Reservatie reservatie1 = new Reservatie(restaurant1, gebruiker, 4, reservationTime1, 3);
            Reservatie reservatie2 = new Reservatie(restaurant2, gebruiker, 2, reservationTime2, 7);

            reservatieManager.AddReservatie(1, 1, reservatie1);
            reservatieManager.AddReservatie(1, 2, reservatie2);

            Console.WriteLine();

            var reservaties = reservatieManager.GetReservationsByReservatieNr(2);
            Console.WriteLine("reservaties");
            Console.WriteLine($"Reservatie: {reservaties.ReservatieNr}, Restaurant: {reservaties.RestaurantInfo.Naam}, Gebruiker: {reservaties.Gebruiker.Naam}, Aantalplaatsen: {reservaties.AantalPlaatsen}, Datum & Uur: {reservaties.DatumUur}, TafelNr: {reservaties.TafelNr}");

            DateTime begin = new DateTime(2023, 12, 25);
            DateTime eind = new DateTime(2023, 12, 30);

            Console.WriteLine();

            var reservatiesGebruiker = reservatieManager.GetAllReservationsByKlantenNr(1, begin, eind);
            Console.WriteLine("reservatiesGebruiker");
            foreach (Reservatie reservation in reservatiesGebruiker)
            {
                Console.WriteLine($"Reservatie: {reservation.ReservatieNr}, Restaurant: {reservation.RestaurantInfo.Naam}, Gebruiker: {reservation.Gebruiker.Naam}, Aantalplaatsen: {reservation.AantalPlaatsen}, Datum & Uur: {reservation.DatumUur}, TafelNr: {reservation.TafelNr}");
            }

            Console.WriteLine();
            //"Hof van Cleve"
            var reservatiesRestaurant = reservatieManager.GetAllReservationsByRestauranNaam("Hof van Cleve", begin, eind);
            Console.WriteLine("reservatiesRestaurant");
            foreach (Reservatie reservation in reservatiesRestaurant)
            {
                Console.WriteLine($"Reservatie: {reservation.ReservatieNr}, Restaurant: {reservation.RestaurantInfo.Naam}, Gebruiker: {reservation.Gebruiker.Naam}, Aantalplaatsen: {reservation.AantalPlaatsen}, Datum & Uur: {reservation.DatumUur}, TafelNr: {reservation.TafelNr}");
            }
        }
    }
}