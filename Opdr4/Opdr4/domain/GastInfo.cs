using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.domain;

public class GastInfo
{
    [Key] public int Id { get; set; }
    
    public Coordinate Coordinate { get; set;}

    public string LaatstBezochteUrl { get; set; }

    public int GastId { get; set; }
    public Gast Gast { get; set; }
}