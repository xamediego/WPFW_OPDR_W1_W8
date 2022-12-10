namespace ConsoleApp3.domain;

public class WerktOnderhoud
{
    public int MedewerkerId { get; set; }
    public Medewerker Medewerker { get; set; }
    public int OnderhoudId { get; set; }
    public Onderhoud Onderhoud { get; set;}
}