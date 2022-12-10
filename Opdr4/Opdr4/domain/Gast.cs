using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp3.domain;

public class Gast : Gebruiker
{
    public int Credits { get; set; }
    public DateTime GeboorteDatum { get; set; }
    public DateTime EersteBezoek { get; set;}
    
    public GastInfo Info { get; set; }
    
    public int ? BegeleiderId { get; set; }
    public Gast Begeleider { get; set;}
    
    public int ? AttractieId { get; set;}
    public Attractie Favoriet;

   public List<Reservering> Reserveringen { get; set; }
    
    protected Gast()
    {
    }

    public Gast(string email) : base(email)
    {
    }

}