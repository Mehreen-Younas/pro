using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class Chef
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Nationality { get; set; }
        

        // Books
        public List<Pasta> Pastas { get; set; }
    }
}
