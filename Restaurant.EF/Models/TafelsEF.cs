using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Models
{
    public class TafelsEF
    {
        public TafelsEF()
        {
        }

        public TafelsEF(int aantalPlaatsen)
        {
            AantalPlaatsen = aantalPlaatsen;
        }

        public TafelsEF(int aantalPlaatsen, RestaurantEF restaurant)
        {
            AantalPlaatsen = aantalPlaatsen;
            Restaurant = restaurant;
        }

        public TafelsEF(int id, int aantalPlaatsen, RestaurantEF restaurant)
        {
            Id = id;
            AantalPlaatsen = aantalPlaatsen;
            Restaurant = restaurant;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int AantalPlaatsen { get; set; }
        [Required]
        public RestaurantEF Restaurant { get; set; }
    }
}
