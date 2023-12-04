using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class ReservatiesEF
    {
        public ReservatiesEF()
        {
            // Default constructor
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservatieNummer { get; set; }


    }
}
