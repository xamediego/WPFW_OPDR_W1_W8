namespace ConsoleApp3.domain;

public class Medewerker : Gebruiker
{
    
    public ICollection<CoordineertOnderhoud> Coordineert { get; set; }
    
    public ICollection<WerktOnderhoud> Werkt { get; set; }
    
    protected Medewerker()
    {
    }

    public Medewerker(string email) : base(email)
    {
    }
}
