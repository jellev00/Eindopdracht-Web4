using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Models
{
    public class Reservatie
    {
        public Reservatie(Restaurant restaurantInfo, Gebruiker gebruiker, int aantalPlaatsen, DateTime datum, TimeSpan uur, int tafelNr)
        {
            _restaurantInfo = restaurantInfo;
            _gebruiker = gebruiker;
            _aantalPlaatsen = aantalPlaatsen;
            _datum = datum;
            _uur = uur;
            _tafelNr = tafelNr;
        }

        public Reservatie(int reservatieNr, Restaurant restaurantInfo, Gebruiker gebruiker, int aantalPlaatsen, DateTime datum, TimeSpan uur, int tafelNr)
        {
            _reservatieNr = reservatieNr;
            _restaurantInfo = restaurantInfo;
            _gebruiker = gebruiker;
            _aantalPlaatsen = aantalPlaatsen;
            _datum = datum;
            _uur = uur;
            _tafelNr = tafelNr;
        }

        private int _reservatieNr;
        public int ReservatieNr
        {
            get
            {
                return _reservatieNr;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("ReservatieNr is niet geldig!");
                }
                else
                {
                    _reservatieNr = value;
                }
            }
        }

        private Restaurant _restaurantInfo;
        public Restaurant RestaurantInfo
        {
            get 
            { 
                return _restaurantInfo; 
            }
            set
            {
                _restaurantInfo = value;
            }
        }

        private Gebruiker _gebruiker;
        public Gebruiker Gebruiker
        {
            get
            {
                return _gebruiker;
            }
            set
            {
                if (value == null)
                {
                    throw new ReservatieException("Gebruiker is niet geldig!");
                }
                else
                {
                    _gebruiker = value;
                }
            }
        }

        private int _aantalPlaatsen;
        public int AantalPlaatsen
        {
            get
            {
                return _aantalPlaatsen;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("AantalPlaatsen is niet geldig");
                }
                _aantalPlaatsen = value;
            }
        }

        private DateTime _datum;
        public DateTime Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                if (value.Date < DateTime.Today)
                {
                    throw new ReservatieException("Datum moet vandaag of in de toekomst zijn.");
                }

                _datum = value;
            }
        }

        private TimeSpan _uur;
        public TimeSpan Uur
        {
            get
            {
                return _uur;
            }
            set
            {
                DateTime combinedDateTime = _datum.Add(value);

                if (combinedDateTime < DateTime.Now)
                {
                    throw new ReservatieException("De combinatie van Datum en Uur mag niet in het verleden zijn.");
                }

                _uur = value;
            }
        }

        private int _tafelNr;
        public int TafelNr
        {
            get
            {
                return _tafelNr;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ReservatieException("ReservatieNr is niet geldig!");
                }
                else
                {
                    _reservatieNr = value;
                }
            }
        }
    }
}
