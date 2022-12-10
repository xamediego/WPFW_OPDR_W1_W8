using System.ComponentModel.DataAnnotations;
using WebApplication2.Data;

namespace WebApplication2.Models;

public class Attractie
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Engheid { get; set; }
    public DateTime Bouwjaar { get; set;}

    public ICollection<UserLikedAttraction> Likes { get; set; } = new List<UserLikedAttraction>();

    public int aantalLikes()
    {
        return Likes.Count;
    }
    
}