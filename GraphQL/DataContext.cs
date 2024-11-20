using GraphQL.Models;
using Microsoft.EntityFrameworkCore;
namespace GraphQL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
    : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
