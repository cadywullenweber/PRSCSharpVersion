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


        public PRSDb() 
        { 
        }
        public PRSDb(DbContextOptions<PRSDb> options) : base(options) 
        {
        }
    }
}
