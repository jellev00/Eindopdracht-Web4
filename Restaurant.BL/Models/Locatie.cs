using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BL.Models
{
    public class Locatie
    {
        public Locatie(string postcode, string gemeentenaam, string straatnaam, string huisnummerlabel)
        {
            _postcode = postcode;
            _gemeentenaam = gemeentenaam;
            _straatnaam = straatnaam;
            _huisnummerlabel = huisnummerlabel;
        }

        private string _postcode;
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                if (value.Length != 4)
                {
                    throw new ArgumentException("Postcode moet bestaan uit 4 cijfers.");

                }
                _postcode = value;
            }
        }

        private string _gemeentenaam;
        public string Gemeentenaam
        {
            get { return _gemeentenaam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Gemeentenaam is verplicht en mag niet leeg zijn.");
                }

                _gemeentenaam = value;
            }
        }

        private string _straatnaam;
        public string Straatnaam
        {
            get { return _straatnaam; }
            set { _straatnaam = value; }
        }

        private string _huisnummerlabel;

        public string Huisnummerlabel
        {
            get { return _huisnummerlabel; }
            set { _huisnummerlabel = value; }
        }
    }
}
