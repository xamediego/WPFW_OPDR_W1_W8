//using OrigineelDataLezer;

using FunctioneelDataLezer;

class MainKlasse
{
    private static int Engheid(Attractie attractie)
    {
        switch (attractie)
        {
            case Attractie.Draaimolen d:
                return 5 + d.DraaiSnelheid;
            
            case Attractie.Rollercoaster r:
                var engheid = r.Lengte / 100;
                
                return engheid > 10 ? 10 : engheid;
        }

        return 0;
    }

    public static void Main(string[] args)
    {
        foreach (var attractie in AttractieDataLezer.Lees().Attracties)
        {
            Console.WriteLine(attractie.Naam + " uit " + attractie.BouwDatum + " [" + attractie.LengteBeperking + "]");
            Console.WriteLine("Engheidsfactor: " + Engheid(attractie));
        }
    }
}