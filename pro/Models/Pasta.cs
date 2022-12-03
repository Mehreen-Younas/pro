using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class Pasta
    {
        
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string URL { get; set; }

        
    }
}
