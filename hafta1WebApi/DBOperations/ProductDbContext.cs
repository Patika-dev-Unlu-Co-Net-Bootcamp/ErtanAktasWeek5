using hafta1WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace hafta1WebApi.DBOperations
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
    }
}
