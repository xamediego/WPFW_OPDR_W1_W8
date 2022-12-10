namespace ConsoleApplication1.Map
{
    public class KaartItem : Tekenbaar
    {

        private Kaart kaart;
        
        private Coordinaat _locatie;

        public KaartItem(Kaart kaart, Coordinaat locatie)
        {
            this.kaart = kaart;
            _locatie = locatie;
        }
        
        public Coordinaat Locatie { get; set;}
        
        public char Karakter { get; }
        
        public void TekenConsole(ConsoleTekener t)
        
        {
            t.SchrijfOp(_locatie, "A");
        }
    }
}