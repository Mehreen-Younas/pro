using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using pro.Models;

namespace pro.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Pasta> Pasta { get; set; }
        public DbSet<Chef> Chef { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PastaChef> PastaChef { get; set; }
        public DbSet<Shape> Shape { get; set; }



    }
}
