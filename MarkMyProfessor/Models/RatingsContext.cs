using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MarkMyProfessor.Models
{
    public class RatingsContext : DbContext
    {
        public RatingsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Professor> Professors { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
