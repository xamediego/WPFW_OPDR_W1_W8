using System;

namespace ConsoleApplication1.Authentication
{
    public class Guest : User
    {
        
        public int Rating { get; set; }
        public int Boete { get; set; }
        public DateTime dfb { get; set; }

        public void Bezoek()
        {
            
        }

        public void VipBezoek()
        {
            
        }

        public void GeefStraf(string daden)
        {
            
        }
        
    }
}