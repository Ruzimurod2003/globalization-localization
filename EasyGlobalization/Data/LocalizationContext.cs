using EasyGlobalization.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EasyGlobalization.Data
{
    public class LocalizationContext : DbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options)
            : base(options)
        {
            // Database.EnsureCreated();
        }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}
