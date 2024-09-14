using DotNet8WebApi.TwilioExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.TwilioExample.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Tbl_User> Tbl_Users { get; set; }
        public DbSet<Tbl_Setup> Tbl_Setups { get; set; }
    }
}
