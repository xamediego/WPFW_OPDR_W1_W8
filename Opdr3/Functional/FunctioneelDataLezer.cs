namespace FunctioneelDataLezer;

using System.Collections.Immutable;

static class Functies
{
    public static Func<(T1, T2), T> Unsplat<T1, T2, T>(Func<T1, T2, T> functie) => tuple => functie(tuple.Item1, tuple.Item2);
	
	private static Func<(T1, T2), T> SP<T1, T2, T>(Func<T1, T2, T> functie) => ((T1 t1, T2 t2) tuple) => functie(tuple.t1, tuple.t2);

    public static Func<T1, T3> Combineer<T1, T2, T3>(
        Func<T2, T3> f2, 
        Func<T1, T2> f1) 
        => (T1 t1) => f2(f1(t1));

    public static Func<TIn, TOutOut> CombineerOutputs<TIn, TOut, TOutOut>(Func<TIn, TOut> f1, Func<TIn, TOut> f2,
        Func<TOut, TOut, TOutOut> combiner) =>
        (TIn i) => combiner(f1(i), f2(i));

    public static Func<T1, (T2, T1)> TellerPrefix<T1, T2>(Func<T2> f) => (T1 t1) => (f(), t1);

    public static Func<T1, T2, T3> T<T1, T2, T3>(Func<(T2, T1), T3> f) => (t1, t2) => f((t2, t1));
    
}

static class Lezer<T>
{
    public static T[] Lees(string fileName, Func<int, string, T> Functie) =>
        ElkeRegel(
            Filter(
                Functies.Combineer(
                    a => !a,
                    Functies.CombineerOutputs<string, bool, bool>(
                        IsLegeRegel,
                        IsCommentaar,
                        (a, b) => a || b
                    )
                ),
                File.ReadAllLines(fileName)
            ),
            Functies.Combineer<string, (int, string), T>(
                Functies.Unsplat<int, string, T>(Functie), 
                Functies.TellerPrefix<string, int>(MetNummer())
            )
        );
        
    private static Func<int> MetNummer(){
        int _nummer = 0;
			
        Func<int> f = () => _nummer++;
        
        return f;
    }
    
    public static string[] Filter(Func<string, bool> f, string[] args) => args switch
    {
        [] => new string[] { },
        [string s, ..] when f(s) => new[] { s }.Concat(Filter(f, args[1..]))
            .ToArray(), // Opdracht (c): wat moet hier komen? (tip: kijk naar ElkeRegel)
        _ => Filter(f, args[1..])
    };

    public static T[] ElkeRegel(string[] regels, Func<string, T> functie) => regels switch
    {
        [] => new T[] { },
        _ => new T[] { functie(regels[0]) }.Concat(ElkeRegel(regels[1..], functie)).ToArray()
    };
    // Opdracht (extra): De methoden Filter en ElkeRegel kunnen abstracter door ze generic te maken, zodat ze voor allerlei soorten lijsten werken (hernoem ElkeRegel naar Map)

    private static bool IsLegeRegel(string s) => s.Trim() == "";
    private static bool IsCommentaar(string s) => s.StartsWith("#");
}

public readonly record struct LengteBeperking(int? MinimaleLengte, int? MaximaleLengte)
{
    public LengteBeperking ZonderMinimum() => this with { MinimaleLengte = null };
    public LengteBeperking ZonderMaximum() => this with { MaximaleLengte = null };
}

// Opdracht (d): schrijf de Attractie klasse
internal abstract record Attractie
{
    public string Naam;
    public string BouwDatum;

    public abstract LengteBeperking LengteBeperking { get; }

    private Attractie(string naam, string bouwDatum)
    {
        Naam = naam;
        BouwDatum = bouwDatum;
    }

    public record Rollercoaster : Attractie
    {
        public int Lengte { get; set; }

        public Rollercoaster(string naam, string bouwDatum, int lengte) : base(naam, bouwDatum)
        {
            Lengte = lengte;
        }

        public override LengteBeperking LengteBeperking { get; }
    }

    public record Draaimolen : Attractie
    {
        public int DraaiSnelheid { get; set; }

        public Draaimolen(string naam, string bouwDatum, int draaiSnelheid) : base(naam, bouwDatum)
        {
            DraaiSnelheid = draaiSnelheid;
        }

        public override LengteBeperking LengteBeperking { get; }
    }
}

readonly record struct AttractieContext(ImmutableList<Attractie> Attracties)
{
    public AttractieContext NieuweAttractie(Attractie attractie) =>
        this with { Attracties = Attracties.Add(attractie) };
}

static class AttractieDataLezer
{
    private static Attractie?[] LeesUitBestand() => Lezer<Attractie?>.Lees("attractie_data.csv",
        (int i, string regel) => regel.Split(",") switch
        {
            [string Naam, "Kapot", _, _, _, _] when Naam.StartsWith("") =>
                null,
            [string Naam, _, "Rollercoaster", string BouwDatum, string Lengte, _] =>
                new Attractie.Rollercoaster(Naam, BouwDatum, int.Parse(Lengte)),
            [string Naam, _, "Draaimolen", string BouwDatum, _, string DraaiSnelheid] =>
                new Attractie.Draaimolen(Naam, BouwDatum, int.Parse(DraaiSnelheid)),
            _ => throw new Exception("Leesfout!")
        });

    private static AttractieContext Verzamel(Attractie?[] attracties) => attracties switch
    {
        [] => new AttractieContext { Attracties = ImmutableList<Attractie>.Empty },
        [null, ..] => Verzamel(attracties[1..]),
        [Attractie a, ..] => Verzamel(attracties[1..]).NieuweAttractie(a)
    };

    public static AttractieContext Lees() => Verzamel(LeesUitBestand());
}
