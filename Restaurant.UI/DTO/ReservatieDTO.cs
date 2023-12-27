using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.UI.DTO
{
    public class ReservatieDTO
    {
        public ReservatieDTO(int aantalPlaatsen, DateTime datumUur)
        {
            AantalPlaatsen = aantalPlaatsen;
            DatumUur = datumUur;
        }

        public int AantalPlaatsen { get; set; }
        public DateTime DatumUur { get; set; }
    }
}
