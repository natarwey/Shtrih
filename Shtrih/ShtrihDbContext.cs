using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shtrih.MainWindow;

namespace Shtrih
{
    internal class ShtrihDbContext : DbContext
    {
        public ShtrihDbContext() : base("name=ShtrihDbConnectionString") { }
        public DbSet<Product> Product { get; set; }
    }
}
