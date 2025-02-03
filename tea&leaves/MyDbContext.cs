using tea_leaves.Models;
using Microsoft.EntityFrameworkCore;
namespace tea_leaves
{
    public class MyDbContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            string? connectionString = _configuration.GetConnectionString(
            "MyConnection");
            if (connectionString != null)
            {
                optionsBuilder.UseMySQL(connectionString);
            }
        }
        public DbSet<Products> Products { get; set; }
        public DbSet<Contact> Contact {  get; set; }
    }
}
