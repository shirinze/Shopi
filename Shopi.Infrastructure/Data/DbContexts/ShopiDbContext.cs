using Microsoft.EntityFrameworkCore;
using Shopi.DomainModel.Models;
using Shopi.DomainModel.Models.ProductAggrigate;


namespace Shopi.Infrastructure.Data.DbContexts;

public class ShopiDbContext(DbContextOptions options):DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Sho");
        #region User

        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().Property(x => x.Name).HasMaxLength(100);
        modelBuilder.Entity<UserEntity>().Property(x => x.LastName).HasMaxLength(100);
        modelBuilder.Entity<UserEntity>().Property(x => x.Phone).HasMaxLength(20);
        modelBuilder.Entity<UserEntity>().HasQueryFilter(x => !x.IsDeleted);

        #endregion

        #region City
        modelBuilder.Entity<City>().HasKey(x => x.Id);
        modelBuilder.Entity<City>().Property(x => x.Name).HasMaxLength(50);
        modelBuilder.Entity<City>().HasData(
            [
                new City { Id = 1, Name = "Tehran"},
                new City { Id = 2, Name = "Mashhad"},
                new City { Id = 3, Name = "Tabriz"},
                new City { Id = 4, Name = "Shiraz"},
                new City { Id = 5, Name = "Ahvaz"},

            ]);
        #endregion

        #region Product

        modelBuilder.Entity<Gold>().Property(g => g.Price).HasPrecision(18, 2);

        modelBuilder.Entity<Phone>().Property(p => p.Price).HasPrecision(18, 2);

        modelBuilder.Entity<Shoes>().Property(s => s.Price).HasPrecision(18, 2);

        modelBuilder.Entity<Shoes>().UseTpcMappingStrategy();
        modelBuilder.Entity<Gold>().UseTpcMappingStrategy();
        modelBuilder.Entity<Phone>().UseTpcMappingStrategy();
        #endregion

        base.OnModelCreating(modelBuilder);
    }
}
