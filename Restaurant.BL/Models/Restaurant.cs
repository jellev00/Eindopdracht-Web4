using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.BL.Exceptions;

namespace Restaurant.BL.Models
{
    public class Restaurant
    {
        //public Beheerder(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
        //{
        //    _naam = naam;
        //    _locatie = locatie;
        //    _keuken = keuken;
        //    _contactgegevens = contactgegevens;
        //}

        public Restaurant(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens)
        {
            _naam = naam;
            _locatie = locatie;
            _keuken = keuken;
            _contactgegevens = contactgegevens;
        }

        private string _naam;
        public string Naam
        {
            get
            {
                return _naam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new RestaurantException("Naam is niet geldig!");
                }
                _naam = value;
            }
        }

        private Locatie _locatie;
        public Locatie Locatie
        {
            get
            {
                return _locatie;
            }
            set
            {
                if (value == null)
                {
                    throw new RestaurantException("Locatie is niet geldig !");
                }
                else
                {
                    _locatie = value;
                }
            }
        }

        private string _keuken;
        public string Keuken
        {
            get
            {
                return _keuken;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Split(' ').Length > 1)
                {
                    throw new RestaurantException("Keuken mag maar één woord bevatten.");
                }

                _keuken = value;
            }
        }

        private Contactgegevens _contactgegevens;
        public Contactgegevens Contactgegevens
        {
            get
            {
                return _contactgegevens;
            }
            set
            {
                if (value == null)
                {
                    throw new RestaurantException("Contactgegevens zijn niet geldig !");
                }
                else
                {
                    _contactgegevens = value;
                }
            }
        }
    }
}
