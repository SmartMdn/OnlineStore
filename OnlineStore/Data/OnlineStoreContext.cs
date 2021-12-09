using Microsoft.EntityFrameworkCore;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Data
{
    public sealed class OnlineStoreContext : DbContext
    {
        public OnlineStoreContext(DbContextOptions<OnlineStoreContext> options ) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
