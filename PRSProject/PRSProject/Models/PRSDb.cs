using Microsoft.EntityFrameworkCore;

namespace PRSProject.Models
{
    public class PRSDb : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Request> Requests {get;set;}
        public DbSet<RequestLine> RequestLines { get; set; }    
        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PRSDb;Integrated Security=true");
            } 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().Property(p => p.Status).HasDefaultValue("NEW");
            modelBuilder.Entity<Request>().Property(d => d.DeliveryMode).HasDefaultValue("Pickup");
            base.OnModelCreating(modelBuilder);
        }
        public PRSDb() 
        { 
        }
        public PRSDb(DbContextOptions<PRSDb> options) : base(options) 
        {
        }
    }
}
