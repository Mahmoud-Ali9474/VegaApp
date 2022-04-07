using Microsoft.EntityFrameworkCore;
using Vega.API.Models;

namespace Vega.API.Data
{
    public class VegaDbContext : DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> option) : base(option)
        { }

        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Make> Makes { get; set; }

    }
}