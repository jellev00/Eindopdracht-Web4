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

        public Restaurant()
        {

        }

        public Restaurant(string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens, bool status)
        {
            _naam = naam;
            _locatie = locatie;
            _keuken = keuken;
            _contactgegevens = contactgegevens;
            _status = status;
        }

        public Restaurant(int id, string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens, bool status)
        {
            _id = id;
            _naam = naam;
            _locatie = locatie;
            _keuken = keuken;
            _contactgegevens = contactgegevens;
            _status = status;
        }

        public Restaurant(int id, string naam, Locatie locatie, string keuken, Contactgegevens contactgegevens, bool status, List<Tafel> tafel, List<Reservatie> reservatie) : this(id, naam, locatie, keuken, contactgegevens, status)
        {
            _tafel = tafel;
            _reservatie = reservatie;
        }

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new RestaurantException("Id is niet geldig!");
                }
                else
                {
                    _id = value;
                }
            }
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

        private bool _status;

        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        private List<Tafel> _tafel;
        public List<Tafel> Tafel
        {
            get
            {
                return _tafel;
            }
            set
            {
                if (value == null)
                {
                    throw new RestaurantException("Tafels kunnen niet null zijn");
                }
                else
                {
                    _tafel = value;
                }
            }
        }

        private List<Reservatie> _reservatie;
        public List<Reservatie> Reservatie
        {
            get
            {
                return _reservatie;
            }
            set
            {
                if (value == null)
                {
                    throw new RestaurantException("Reservaties kunnen niet null zijn");
                }
                else
                {
                    _reservatie = value;
                }
            }
        }
    }
}
