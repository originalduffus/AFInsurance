using AFInsurance.Models.DB;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }


    }
}