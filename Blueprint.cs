namespace WorldOfZuul
{
    public abstract class Blueprint
    {
        public string name;
        public char symbol;
        public int count;
        public List<int> resources;
        public Blueprint(string name, char symbol, int count, List<int> resources)
        {
            this.name = name;
            this.symbol = symbol;
            this.count = count;
            this.resources = resources;
        }
        public abstract Building Build(List<int> coordinates);
    }
    
    public class HouseBlueprint : Blueprint
    {
        int inahbitants;
        int survIndex;

        public HouseBlueprint(string name, char symbol, int count, List<int> resources, int inhabitants, int survIndex) : base(name, symbol, count, resources)
        {
            this.inahbitants = inhabitants;
            this.survIndex = survIndex;
        }

        public override House Build(List<int> coordinates)
        {
            House a = new(this.name, coordinates, this.inahbitants, this.survIndex);
            return a;
        }
    }

    public class IndustrialBlueprint : Blueprint
    {
        int range;
        int impact;
        public IndustrialBlueprint(string name, char symbol, int count, List<int> resources, int range, int impact) : base(name, symbol, count, resources)
        {
            this.range = range;
            this.impact = impact;
        }

        public override Industrial Build(List<int> coordinates)
        {
            Industrial a = new(this.name, this.symbol, coordinates, this.range, this.impact);
            return a;
        }
    }
}