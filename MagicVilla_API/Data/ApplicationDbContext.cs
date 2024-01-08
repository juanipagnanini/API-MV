using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Real",
                    Description = "Detail of the villa...",
                    Rate = 200,
                    Occupants = 5,
                    SquareMeters = 50,
                    ImagenUrl = "",
                    Services = "",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Premium beach view",
                    Description = "Detail of the villa...",
                    Rate = 150,
                    Occupants = 4,
                    SquareMeters = 40,
                    ImagenUrl = "",
                    Services = "",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                }
            );
        }
    }
}
