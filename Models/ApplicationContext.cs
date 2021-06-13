using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestASP.NET.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Footballer> Footballers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { 
            Database.EnsureCreated(); 
        }
    }
}
