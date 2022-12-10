using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;

public class DatabaseContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    protected override void OnConfiguring(DbContextOptionsBuilder b) => b.UseSqlite("Data Source=database.db");

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserLikedAttraction>()
            .HasKey(wo => new { wo.AttractieId, wo.UserId });

        builder.Entity<UserLikedAttraction>()
            .HasOne(wo => wo.User)
            .WithMany(wo => wo.Liked)
            .HasForeignKey(wo => wo.AttractieId);

        builder.Entity<UserLikedAttraction>()
            .HasOne(wo => wo.Attractie)
            .WithMany(wo => wo.Likes)
            .HasForeignKey(wo => wo.UserId);

        base.OnModelCreating(builder);
    }

    public DbSet<Attractie> Attracties { get; set; } = default!;

    public DbSet<UserLikedAttraction> Likes { get; set; } = default!;

    public DbSet<ApplicationUser> ApplicationUser { get; set; } = default!;
}

