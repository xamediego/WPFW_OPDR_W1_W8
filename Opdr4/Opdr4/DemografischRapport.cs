using ConsoleApp3.domain;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3;

class DemografischRapport : Rapport
{
    private DatabaseContext context;
    public DemografischRapport(DatabaseContext context) => this.context = context;
    public override string Naam() => "Demografie";

    public override async Task<string> Genereer()
    {
        string ret = "Dit is een demografisch rapport: \n";
        ret +=
            $"Er zijn in totaal {await AantalGebruikers()} gebruikers van dit platform (dat zijn gasten en medewerkers)\n";
        var dateTime = new DateTime(2000, 1, 1);
        ret += $"Er zijn {await AantalSinds(dateTime)} bezoekers sinds {dateTime}\n";
        if (await AlleGastenHebbenReservering())
            ret += "Alle gasten hebben een reservering\n";
        else
            ret += "Niet alle gasten hebben een reservering\n";
        ret += $"Het percentage bejaarden is {await PercentageBejaarden()}\n";

        ret += $"De oudste gast heeft een leeftijd van {await HoogsteLeeftijd()} \n";

        ret += "De verdeling van de gasten per dag is als volgt: \n";
        var dagAantallen = await VerdelingPerDag();
        var totaal = dagAantallen.Select(t => t.aantal).Max();
        // foreach (var dagAantal in dagAantallen)
        //     ret += $"{ dagAantal.dag }: { new string('#', (int)(dagAantal.aantal / (double)totaal * 20)) }\n";

        foreach (var d in dagAantallen)
            ret += d.dag + ": " + (int)(d.aantal / (double)totaal * 20) + "\n";

        //ret += $"{ await FavorietCorrect() } gasten hebben de favoriete attractie inderdaad het vaakst bezocht. \n";

        return ret;
    }

    private async Task<int> AantalGebruikers() => context
        .Gebruikers.Count();

    private async Task<bool> AlleGastenHebbenReservering() => context
        .Gasten.Include(g => g.Reserveringen)
        .Any(g => g.Reserveringen.Count > 0);

    private async Task<int> AantalSinds(DateTime sinds) => context.Gasten.Count(g => g.EersteBezoek > sinds);

    private async Task<Gast> GastBijEmail(string email) => context.Gasten
        .SingleOrDefault(g => g.Email == email);

    private async Task<Gast?> GastBijGeboorteDatum(DateTime d) => context
        .Gasten
        .SingleOrDefault(g => g.GeboorteDatum == d);

    private async Task<double> PercentageBejaarden()
        => context.Gasten.Average(g => g.GeboorteDatum >= DateTime.Now.AddYears(-65) ? 1.0 : 0.0) * 100;

    private async Task<int> HoogsteLeeftijd() =>
        DateTime.Now.AddYears(-context.Gasten.Min(g => g.GeboorteDatum).Year).Year;

    private async Task<IEnumerable<Gast>> Blut() => context.Gasten
        .Where(g => g.Credits <= 0);

    private async Task<(string dag, int aantal)[]> VerdelingPerDag() =>
        context.Gasten.ToList()
            .GroupBy(g => g.EersteBezoek.DayOfWeek)
            .Select(g => { return (g.Key.ToString(), g.Count()); }).ToArray();

    private async Task<(Gast gast, int aantal)[]> GastenMetActiviteit() => context
        .Gasten.Include(g => g.Reserveringen).ToList().Where(g => g.Reserveringen.Count > 0)
        .Select(g => { return (g, g.Reserveringen.Count); }).ToArray();

    //private async Task<int> FavorietCorrect() => /* ... */;
}