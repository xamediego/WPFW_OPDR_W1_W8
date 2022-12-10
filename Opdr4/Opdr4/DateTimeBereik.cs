using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3;

[Owned]
public class DateTimeBereik
{
    public DateTime Begin { get; set; }
    public DateTime ? Eind { get; set; }

    public bool Eindigt()
    {
        return Eind < DateTime.Now;
    }


    public bool Overlapt(DateTimeBereik that)
    {
        return !(that.Begin > Eind || that.Eind < Begin);
    }
        
}