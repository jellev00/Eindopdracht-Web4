using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Models
{
    public class Gebruiker
    {
        public Gebruiker()
        {
        }
        
        public Gebruiker(string naam, string email, string telefoonNr, Locatie locatie)
        {
            _naam = naam;
            _email = email;
            _telefoonNr = telefoonNr;
            _locatie = locatie;
        }

        public Gebruiker(int klantenNr, string naam, string email, string telefoonNr, Locatie locatie)
        {
            _klantenNr = klantenNr;
            _naam = naam;
            _email = email;
            _telefoonNr = telefoonNr;
            _locatie = locatie;
        }

        public Gebruiker(int klantenNr,  string naam, string email, string telefoonNr, Locatie locatie, List<Reservatie> reservatie) : this(klantenNr, naam, email, telefoonNr, locatie)
        {
            _reservatie = reservatie;
        }

        private int _klantenNr;
        public int KlantenNr
        {
            get
            {
                return _klantenNr;
            }
            set
            {
                if (value <= 0)
                {
                    throw new GebruikerException("KlantenNr is niet geldig!");
                }
                else
                {
                    _klantenNr = value;
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
                    throw new GebruikerException("Naam is niet geldig!");
                }
                else
                {
                    _naam = value;
                }
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value == "string")
                {
                    _email = value;
                }
                if ((string.IsNullOrWhiteSpace(value)) || (!value.Contains('@')))
                {
                    throw new GebruikerException("Email is niet geldig!");
                }
                else
                {
                    _email = value;
                }
            }
        }

        private string _telefoonNr;
        public string TelefoonNr
        {
            get
            {
                return _telefoonNr;
            }
            set
            {
                if (value == "string")
                {
                    _telefoonNr = value;
                }
                if (!string.IsNullOrWhiteSpace(value) && System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
                {
                    _telefoonNr = value;
                }
                else
                {
                   throw new GebruikerException("TelefoonNr mag alleen nummers bevatten!");
                }
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
                    throw new GebruikerException("Locatie is niet geldig!");
                }
                else
                {
                    _locatie = value;
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
                    throw new GebruikerException("Reservatie kan niet null zijn");
                }
                else
                {
                    _reservatie = value;
                }
            }
        }
    }
}
