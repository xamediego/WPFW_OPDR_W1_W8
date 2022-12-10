using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class UserLikedAttraction
{
    [Key] public int UserId { get; set; }
    public ApplicationUser User { get; set; }
    [Key] public int AttractieId { get; set; }
    public Attractie Attractie { get; set; }
}