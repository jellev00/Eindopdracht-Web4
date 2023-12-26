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

        public Reservatie()
        {

        }
        public Reservatie(Restaurant restaurantInfo, Gebruiker gebruiker, int aantalPlaatsen, DateTime datumUur, int tafelNr)
        {
            _restaurantInfo = restaurantInfo;
            _gebruiker = gebruiker;
            _aantalPlaatsen = aantalPlaatsen;
            _datumUur = datumUur;
            _tafelNr = tafelNr;
        }

        public Reservatie(int reservatieNr, Restaurant restaurantInfo, Gebruiker gebruiker, int aantalPlaatsen, DateTime datumUur, int tafelNr)
        {
            _reservatieNr = reservatieNr;
            _restaurantInfo = restaurantInfo;
            _gebruiker = gebruiker;
            _aantalPlaatsen = aantalPlaatsen;
            _datumUur = datumUur;
            _tafelNr = tafelNr;
        }

        public Reservatie(int aantalPlaatsen, DateTime datumUur)
        {
            _aantalPlaatsen = aantalPlaatsen;
            _datumUur = datumUur;
        }

        public Reservatie(int aantalPlaatsen, DateTime datumUur, int tafelNr)
        {
            _aantalPlaatsen = aantalPlaatsen;
            _datumUur = datumUur;
            _tafelNr = tafelNr;
        }

        public Reservatie(int reservatieNr, int aantalPlaatsen, DateTime datumUur)
        {
            _reservatieNr = reservatieNr;
            _aantalPlaatsen = aantalPlaatsen;
            _datumUur = datumUur;
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

        private DateTime _datumUur;

        public DateTime DatumUur
        {
            get
            {
                return _datumUur;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ReservatieException("De combinatie van Datum en Uur mag niet in het verleden zijn.");
                }

                DateTime opening = DateTime.Today.Add(new TimeSpan(10, 0, 0));
                DateTime sluiting = DateTime.Today.Add(new TimeSpan(23, 0, 0));

                if (value.TimeOfDay < opening.TimeOfDay || value.TimeOfDay > sluiting.TimeOfDay)
                {
                    throw new ReservatieException($"De combinatie van Datum en Uur moet tussen {opening.TimeOfDay} en {sluiting.TimeOfDay} liggen.");
                }

                _datumUur = value;
            }
        }

        public void EindUur()
        {
            DatumUur.AddHours(1).AddMinutes(30);
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
