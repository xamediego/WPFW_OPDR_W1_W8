using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models;

public class ApplicationUser : IdentityUser<int>
{
    [JsonIgnore]
    public Gender Gender { get; set; }
    
    [JsonIgnore]
    public ICollection<UserLikedAttraction> Liked { get; set; }
}