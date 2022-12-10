using ConsoleApp3.domain;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3;

public class DatabaseContext : DbContext
{
    private static bool _created = false;

    public DatabaseContext()
    {
        if (!_created)
        {
            _created = true;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder b) => b.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Prevent cascade delete for reservations when deleting a guest
        builder.Entity<Gast>()
            .HasMany(g => g.Reserveringen)
            .WithOne(e => e.Gast)
            .OnDelete(DeleteBehavior.ClientNoAction);

        //Onderhoud => Werknemer many to many (jpa looks better tbh)
        builder.Entity<WerktOnderhoud>()
            .HasKey(wo => new { wo.MedewerkerId, wo.OnderhoudId });
        builder.Entity<WerktOnderhoud>()
            .HasOne(wo => wo.Medewerker)
            .WithMany(wo => wo.Werkt)
            .HasForeignKey(wo => wo.OnderhoudId);
        builder.Entity<WerktOnderhoud>()
            .HasOne(wo => wo.Onderhoud)
            .WithMany(wo => wo.Werkers)
            .HasForeignKey(wo => wo.MedewerkerId);

        builder.Entity<CoordineertOnderhoud>()
            .HasKey(wo => new { wo.MedewerkerId, wo.OnderhoudId });
        builder.Entity<CoordineertOnderhoud>()
            .HasOne(wo => wo.Medewerker)
            .WithMany(wo => wo.Coordineert)
            .HasForeignKey(wo => wo.OnderhoudId);
        builder.Entity<CoordineertOnderhoud>()
            .HasOne(wo => wo.Onderhoud)
            .WithMany(wo => wo.Coordinators)
            .HasForeignKey(wo => wo.MedewerkerId);
    }

    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

    public async Task<bool> Boek(Attractie attractie, DateTimeBereik d, Gast gast)
    {
        bool returnVal;


        await Semaphore.WaitAsync();
        try
        {
            if (gast.Credits <= 0)
            {
                Console.WriteLine("Not enough Credits");
                returnVal = false;
            }
            else
            {
                if (await attractie.Vrij(this, d))
                {
                    Console.WriteLine("Overlapping reservation");
                    returnVal = false;
                }
                else
                {
                    //transaction
                    var transaction = Database.BeginTransaction();

                    var t = Gasten.SingleOrDefault(g => g.Id == gast.Id);
                    t.Credits--;

                    //Updates transaction to db
                    transaction.Commit();
                    
                    // new reservation
                    Reservering reservering = new Reservering
                    {
                        Gast = gast,
                        Attractie = attractie,
                        DateTimeBereik = d
                    };

                    Reserveringen.Add(reservering);
                    await SaveChangesAsync();

                    returnVal = true;
                }
            }
        }
        finally
        {
            Semaphore.Release();
        }

        return returnVal;
    }

    public DbSet<Gebruiker> Gebruikers { get; set; }
    public DbSet<Gast> Gasten { get; set; }
    public DbSet<GastInfo> GastenInfo { get; set; }

    public DbSet<Medewerker> Medewerkers { get; set; }
    public DbSet<Onderhoud> Onderhoud { get; set; }
    public DbSet<Reservering> Reserveringen { get; set; }
    public DbSet<Attractie> Attracties { get; set; }
}