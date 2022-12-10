namespace ConsoleApplication1.Map
{
    public class Attractie : KaartItem
    {
        
        private char Karakter { get; }
        
        private int? minimaleLengee { get; set; }
        private int _angstLevel { get; set; }
        private string _naan { get; set; }
        
        public AttractieCategorie Category { get; set;}


        public Attractie(Kaart kaart, Coordinaat locatie) : base(kaart, locatie)
        {
            
        }
    }
}