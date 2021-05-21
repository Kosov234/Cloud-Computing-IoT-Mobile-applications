using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mobile_Lab3.Web.Models;

namespace Mobile_Lab3.Web.Context
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected AzureDbContext()
        {

        }

        public DbSet<Person> People { get; set; }
    }
}
