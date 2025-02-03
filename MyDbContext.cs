using EDP_project.Models;
using Microsoft.EntityFrameworkCore;
namespace EDP_project
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
        public required DbSet<CateringOrder> CateringOrders { get; set; }
        public required DbSet<ClassBooking> ClassBookings { get; set; }
    }
}