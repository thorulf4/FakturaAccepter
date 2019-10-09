using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FakturaAccepter.Data;

namespace FakturaAccepter.Models
{
    public class FakturaContext : DbContext
    {
        public FakturaContext (DbContextOptions<FakturaContext> options)
            : base(options)
        {
        }

        public DbSet<FakturaAccepter.Data.Faktura> Faktura { get; set; }
    }
}
