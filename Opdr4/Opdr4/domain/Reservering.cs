using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace ConsoleApp3.domain;

public class Reservering
{
    [Key] public int Id { get; set; }

    public DateTimeBereik DateTimeBereik { get; set; }

    public int? GastId { get; set; } 
    public virtual Gast Gast { get; set; }

    public int? AttractieId { get; set; } 
    public virtual Attractie Attractie { get; set; }
}
