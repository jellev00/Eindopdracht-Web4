using Restaurant.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Models
{
    public class Contactgegevens
    {
        public Contactgegevens(int telefoonNr, string email)
        {
            _telefoonNr = telefoonNr;
            _email = email;
        }

        private int _telefoonNr;
        public int TelefoonNr
        {
            get
            {
                return _telefoonNr;
            }
            set
            {
                if (IsNumeric(value))
                {
                    _telefoonNr = value;
                }
                else
                {
                    throw new ContactgegevensException("TelefoonNr mag alleen nummers bevatten!");
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
                if ((string.IsNullOrWhiteSpace(value)) || (!value.Contains('@')))
                {
                    throw new ContactgegevensException("Email is niet geldig!");
                }
                else
                {
                    _email = value;
                }
            }
        }

        // Hulp methode om te kijken of het numers zijn
        private bool IsNumeric(int value)
        {
            return value >= 0;
        }
    }
}
