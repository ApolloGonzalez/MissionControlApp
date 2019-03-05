using Microsoft.EntityFrameworkCore;
using MissionControlApp.API.Models;

namespace MissionControlApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Value> Values { get; set; }
    }
}