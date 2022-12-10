using System.ComponentModel.DataAnnotations;

namespace ConsoleApp3.domain;

public class Gebruiker
{
    [Key]
    public int Id { get; set; }
    
    public string Email { get; set; }

    protected Gebruiker(){}
    
    public Gebruiker(string email)
    {
        Email = email;
    }
}