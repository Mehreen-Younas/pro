using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pro.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
        // Book
        [ForeignKey("Pastas")]
        public int PastaId { get; set; }
        public virtual Pasta Pastas { get; set; }
        // User
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public virtual IdentityUser Users { get; set; }


    }
}