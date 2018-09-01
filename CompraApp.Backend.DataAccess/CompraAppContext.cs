using CompraApp.Backend.Domain;
using CompraApp.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.DataAccess
{
    public class CompraAppContext : DbContext
    {
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<Buyer> Buyers { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
    }
}
