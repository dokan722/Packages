using Packages.Models;
using Microsoft.EntityFrameworkCore;

namespace Packages.Data
{
    public class PackagesContext : DbContext
    {
        public PackagesContext(DbContextOptions<PackagesContext> options) : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
    }
}