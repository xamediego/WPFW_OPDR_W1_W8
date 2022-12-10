using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.domain;

public class Attractie
{
    [Key] public int Id { get; set; }

    public List<Reservering> Reserveringen { get; set; }

    public List<Gast> Favorited { get; set; }

    public List<Onderhoud> Onderhoud { get; set; }

    public string Naam { get; set; }

    protected Attractie()
    {
    }

    public Attractie(string naam)
    {
        Naam = naam;
    }

    public Task<bool> OnderhoudBezig(DatabaseContext db) => Task.FromResult(db.Onderhoud
        .ToList()
        .Where(o => o.AttractieId == Id)
        .Any(o => o.DateTimeBereik.Overlapt(new DateTimeBereik{Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1)})));

    public Task<bool> Vrij(DatabaseContext context, DateTimeBereik d) => Task.FromResult(
        context.Reserveringen
        .ToList()
        .Where(r => r.AttractieId == Id)
        .Any(r => r.DateTimeBereik.Overlapt(d)));

    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
}