using pro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace pro.Models
{
    public class Pasta
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int Price { get; set; }
        

        // Publisher
        [ForeignKey("Shapes")]
        public int ShapesId { get; set; }
        public virtual Shape Shapes { get; set; }

        // Author
        public List<Chef> Chefs { get; set; }
    }
}