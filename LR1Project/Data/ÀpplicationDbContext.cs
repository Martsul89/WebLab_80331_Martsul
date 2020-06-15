using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebLab.DAL.Entities;

namespace WebLab.DAL.Data
{
    public class ÀpplicationDbContext : DbContext
    {
        public ÀpplicationDbContext (DbContextOptions<ÀpplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebLab.DAL.Entities.Flower> Flower { get; set; }
    }
}
