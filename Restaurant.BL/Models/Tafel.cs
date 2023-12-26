using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Models
{
    public class Tafel
    {
        public Tafel()
        {
        }

        public Tafel(int aantalPlaatsen)
        {
            _aantalPlaatsen = aantalPlaatsen;
        }

        public Tafel(int aantalPlaatsen, Restaurant restaurant)
        {
            _aantalPlaatsen = aantalPlaatsen;
            _restaurant = restaurant;
        }

        public Tafel(int id, int aantalPlaatsen, Restaurant restaurant)
        {
            _id = id;
            _aantalPlaatsen = aantalPlaatsen;
            _restaurant = restaurant;
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
                    throw new TafelException("Id is niet geldig!");
                }
                else
                {
                    _id = value;
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
                    throw new TafelException("Aantal plaatsen moet groter zijn dan 0!");
                } else
                {
                    _aantalPlaatsen = value;
                }
            }
        }

        private Restaurant _restaurant;
        public Restaurant Restaurant
        {
            get
            {
                return _restaurant;
            }
            set
            {
                if (value == null)
                {
                    throw new TafelException("Restaurant is ongeldig!");
                }
                else
                {
                    _restaurant = value;
                }
            }
        }
    }
}
