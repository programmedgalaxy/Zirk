using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zirk.Models
{
    public class ZirkContext : DbContext
    {
        public ZirkContext (DbContextOptions<ZirkContext> options)
            : base(options)
        {
        }

        public DbSet<Zirk.Models.Datum> Datum { get; set; }
    }
}
