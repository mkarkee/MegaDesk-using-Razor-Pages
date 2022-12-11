using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;

namespace MegaDesk.Data
{
    public class MegaDeskContext : DbContext
    {
        public MegaDeskContext (DbContextOptions<MegaDeskContext> options)
            : base(options)
        {
        }

        public DbSet<DeliveryType>? DeliveryType { get; set; }
        public DbSet<Desk>? Desk { get; set; }
        public DbSet<DeskQuote>? DeskQuote { get; set; }
        public DbSet<DesktopMaterial>? DesktopMaterial { get; set; }
    }
}
