using ConsoleApp3;
using ConsoleApp3.domain;

namespace Opdr4Test;

public class UnitTest1
{
    [Fact]
    public void Overlapt_ShouldReturnFalse_WhenDoesntOverlap()
    {
        DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };
        DateTimeBereik d2 = new DateTimeBereik { Begin = DateTime.Now.AddDays(3), Eind = DateTime.Now.AddDays(2) };

        Assert.False(d1.Overlapt(d2));
    }

    [Fact]
    public async void Vrij_ShouldReturnFalse_WhenAlreadyHasReservation()
    {
        await using var c = new DatabaseContext();
        Attractie at = new Attractie("T");
        DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };

        c.Attracties.Add(at);
        await c.SaveChangesAsync();

        Assert.False(await at.Vrij(c, d1));
    }
    
    [Fact]
    public async void Boek_ShouldReturnTrue_WhenFree()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");
            DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };
            DateTimeBereik d2 = new DateTimeBereik { Begin = DateTime.Now.AddDays(3), Eind = DateTime.Now.AddDays(5) };
            Gast gast = new Gast("t");
            gast.Credits = 100;

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            c.Gasten.Add(gast);
            await c.SaveChangesAsync();

            Reservering reservering = new Reservering
            {
                DateTimeBereik = d1,
                Attractie = at,
                Gast = gast
            };

            c.Reserveringen.Add(reservering);
            await c.SaveChangesAsync();

            Assert.True(await c.Boek(at, d2, gast));
        }
    }

    [Fact]
    public async void Boek_ShouldReturnFalse_WhenNotFree()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");
            DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };
            Gast gast = new Gast("t");
            gast.Credits = 100;

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            c.Gasten.Add(gast);
            await c.SaveChangesAsync();

            Reservering reservering = new Reservering
            {
                DateTimeBereik = d1,
                Attractie = at,
                Gast = gast
            };

            c.Reserveringen.Add(reservering);
            await c.SaveChangesAsync();

            Assert.False(await c.Boek(at, d1, gast));
        }
    }

    [Fact]
    public async void Boek_ShouldReturnFalse_WhenNoCredits()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");
            DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };
            DateTimeBereik d2 = new DateTimeBereik { Begin = DateTime.Now.AddDays(3), Eind = DateTime.Now.AddDays(5) };
            Gast gast = new Gast("t");
            gast.Credits = 0;

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            c.Gasten.Add(gast);
            await c.SaveChangesAsync();

            Reservering reservering = new Reservering
            {
                DateTimeBereik = d1,
                Attractie = at,
                Gast = gast
            };

            c.Reserveringen.Add(reservering);
            await c.SaveChangesAsync();

            Assert.False(await c.Boek(at, d2, gast));
        }
    }

    [Fact]
    public async void Boek_ShouldDecreaseCredits()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");
            DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };

            int credits = 100;

            Gast gast = new Gast("t");
            gast.Credits = credits;

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            c.Gasten.Add(gast);
            await c.SaveChangesAsync();

            await c.Boek(at, d1, gast);
            
            Assert.True(c.Gasten.Find(gast.Id)!.Credits == credits - 1);
        }
    }

    [Fact]
    public async void OnderhoudBezig_ShouldReturnFalse_WhenNoOnderhoud()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            Assert.False(await c.Attracties.FindAsync(at.Id).Result.OnderhoudBezig(c));
        }
    }

    [Fact]
    public async void OnderhoudBezig_ShouldReturnTrue_WhenBezig()
    {
        using (DatabaseContext c = new DatabaseContext())
        {
            Attractie at = new Attractie("T");

            c.Attracties.Add(at);
            await c.SaveChangesAsync();

            DateTimeBereik d1 = new DateTimeBereik { Begin = DateTime.Now, Eind = DateTime.Now.AddDays(1) };
            Onderhoud onderhoud = new Onderhoud { DateTimeBereik = d1, Attractie = at, Probleem = "sneed" };

            c.Onderhoud.Add(onderhoud);
            await c.SaveChangesAsync();
            
            Assert.True(await c.Attracties.FindAsync(at.Id).Result.OnderhoudBezig(c));
        }
    }
}