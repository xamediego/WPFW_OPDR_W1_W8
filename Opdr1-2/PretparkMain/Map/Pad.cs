using System;

namespace ConsoleApplication1.Map
{
    public class Pad : Tekenbaar
    {
        private float? _lengteBerekend;

        public Coordinaat van = new Coordinaat();
        public Coordinaat naar = new Coordinaat();

        public float Lengte()
        {
            _lengteBerekend = (float)Math.Sqrt(
                Math.Pow(naar.X - van.X, 2) +
                Math.Pow(naar.Y - van.Y, 2));
            
            return (float) _lengteBerekend;
        }

        public float Afstand(Coordinaat c)
        {
            var ver = naar - van;

            return Math.Abs(ver.X) + Math.Abs(ver.Y);
        }

        public void TekenConsole(ConsoleTekener t)
        {
            Coordinaat verschil = naar - van;

            for (int i = 1; i < 100; i++)
            {
                t.SchrijfOp(
                    van + new Coordinaat(
                        (int)(verschil.X * ((float)i / 100) + 0.5),
                        (int)(verschil.Y * ((float)i / 100) + 0.5)
                    ),
                    "#");
            }
            
            t.SchrijfOp(
                van + new Coordinaat(
                    (int)(verschil.X * ((float)50 / 100) + 0.5),
                    (int)(verschil.Y * ((float)50 / 100) + 0.5)
                ),
                Float.MetSuffixen(Lengte()));
        }
    }
}