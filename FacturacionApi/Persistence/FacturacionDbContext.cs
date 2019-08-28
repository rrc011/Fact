using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Persistence
{
    public class FacturacionDbContext : DbContext
    {
        public FacturacionDbContext(DbContextOptions<FacturacionDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
    }
}
