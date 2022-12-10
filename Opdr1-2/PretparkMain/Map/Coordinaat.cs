using System;

namespace ConsoleApplication1.Map
{
    public struct Coordinaat
    {
        public readonly int X;
        public readonly int Y;

        public Coordinaat(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Coordinaat operator +(Coordinaat c1, Coordinaat c2)
        {
            return new Coordinaat(c1.X + c2.X, c1.Y + c2.Y);
        }

        public static Coordinaat operator -(Coordinaat c1, Coordinaat c2)
        {
            return new Coordinaat(c1.X - c2.X, c1.Y - c2.Y);
        }
    }
}