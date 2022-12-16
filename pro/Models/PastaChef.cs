using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pro.Models
{
    public class PastaChef
     {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Pastas")]
        public int PastaId { get; set; }
        public Pasta Pastas { get; set; }

        [ForeignKey("Chefs")]
        public int ChefId { get; set; }
        public Chef Chefs { get; set; }
    }
}
       