using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.domain;

public class Onderhoud
{
    [Key]
    public int Id { get; set; }
    
    public DateTimeBereik DateTimeBereik { get; set; }

    public string Probleem { get; set; }
    
    public int AttractieId { get; set;}
    public Attractie Attractie { get; set;}
    
    public ICollection<CoordineertOnderhoud> Coordinators { get; set; }
    
    public ICollection<WerktOnderhoud> Werkers { get; set; }
}