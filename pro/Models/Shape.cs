using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class Shape
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
      

        // Books
        public List<Pasta> Pastas { get; set; }
    }
}
