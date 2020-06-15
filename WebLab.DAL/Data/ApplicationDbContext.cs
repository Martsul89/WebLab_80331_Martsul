using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Entities;

namespace WebLab.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<FlowerGroup> FlowerGroups { get; set; }
    }
}