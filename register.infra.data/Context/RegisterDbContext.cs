using Microsoft.EntityFrameworkCore;
using register.domain.Entities;
using register.domain.Enum;
using System;
using System.Threading.Tasks;

namespace register.infra.data.Context
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext(DbContextOptions<RegisterDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customer { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149087"), "John", "Stout", DateTime.Parse("2000-04-21"), GenderType.Male)),
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149088"), "Mary", "Dunkel", DateTime.Parse("2005-02-12"), GenderType.Female)),
                (new Customer(Guid.Parse("1ed5e347-bd10-411f-89fc-fe7a13149089"), "Jane", "Pilsen", DateTime.Parse("2000-11-02"), GenderType.Male)));

        }
    }
}
