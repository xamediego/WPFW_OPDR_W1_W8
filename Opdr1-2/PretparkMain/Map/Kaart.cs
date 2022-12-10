using System.Collections.Generic;

namespace ConsoleApplication1.Map
{
    public class Kaart
    {
        public readonly int Breedte;
        public readonly int Hoogte;
        
        public readonly IList<Tekenbaar> Drawables = new List<Tekenbaar>();
         
        public Kaart(int breedte, int hoogte)
        {
            Breedte = breedte;
            Hoogte = hoogte;
        }

        public void VoegItemToe(KaartItem item)
        {
            Drawables.Add(item);
        }

        public void VoegPadToe(Pad pad)
        {
            Drawables.Add(pad);
        }

        public void Teken(Tekener t)
        {
            foreach (var drawable in Drawables)
            {
                t.Teken(drawable);
            }
        }
    }
}