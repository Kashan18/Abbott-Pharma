using Microsoft.EntityFrameworkCore;

namespace Abbott_Pharma.Models
{
    public class DBModel:DbContext
    {
        public DBModel(DbContextOptions abc):base(abc)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Tablet> Tablets { get; set; }
        public DbSet<Capsule> Capsules { get; set; }
        public DbSet<Liquid> Liquids { get; set; }

    }
}
