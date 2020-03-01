using Microsoft.EntityFrameworkCore;

namespace WP.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Wedding {get;set;}
        public DbSet<RSVP> RSVP {get;set;}
    }
}