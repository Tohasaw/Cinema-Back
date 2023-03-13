using Cinema.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Cinema.Data.Contexts;
public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<TableEntry> TableEntries { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_configuration.GetConnectionString("Postgres"), o => { o.MigrationsAssembly("Cinema.Web"); });

        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Cinema.Data"));

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Role>().
            HasData(
            new Role() { Id = 1, Name = "admin" },
            new Role() { Id = 2, Name = "cassir" },
            new Role() { Id = 3, Name = "controler" }
            );
        modelBuilder.Entity<User>().
            HasData(
            new User() { Id = 1, Email = "admin", Password = "admin", RoleId = 1 }
            );
        int i = 1;
        int i1 = 1;
        for (int r = 1; r < 9; r++)
        {
            for (int c = 1; c < 32; c++)
            {
                modelBuilder.Entity<Seat>().
                    HasData(
                    new Seat() { Id = i, IsAvailable = true, Column = c, Row = r }
                    );
                i++;
            }
        }


        for (int r = 9; r < 17; r++)
        {
            for (int c = 1; c < 24; c++)
            {
                modelBuilder.Entity<Seat>().
                    HasData(
                    new Seat() { Id = i, IsAvailable = true, Column = c, Row = r }
                    );
                i++;
            }
        }
        var id = 433;
        var column = 1;
        modelBuilder.Entity<Seat>().
            HasData(
            new Seat() { Id = id, IsAvailable = true, Column = column++, Row = 17 }
            );
        id++;
        for (int h = 1; h < 6; h++)
        {
            modelBuilder.Entity<Seat>().
                HasData(
                new Seat() { Id = id, IsAvailable = true, Column = column++, Row = 17 }
                );
            id++;
        }
        modelBuilder.Entity<Seat>().
            HasData(
            new Seat() { Id = id, IsAvailable = true, Column = column++, Row = 17 }
            );
    }

}

