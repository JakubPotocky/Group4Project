using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace WorldOfZuul
{
        // Define a Building class
    public class Building
    {
        // Properties of a building
        public string name;
        public char symbol;
        protected readonly List<int> coordinates;

        // Constructor for the Building class
        public Building (string name, char symbol, List<int> coordinates)
        {
            this.name = name;
            this.symbol = symbol;
            this.coordinates = new List<int>(coordinates);
        }
    }
    
    // Define a House class that inherits from Building
    public class House : Building
    {
        public static Dictionary<List<int>, House> all = new();
        public int inhabitants;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex

        // Constructor for the House class
        public House(string name, List<int> coordinates, int humanCount, int survivabilityIndex) : base(name, 'l', coordinates)
        {
            this.inhabitants = humanCount;
            this.survivabilityIndex = survivabilityIndex;
            all.Add(this.coordinates, this);
        }
    }

    // Define an Industrial class that inherits from Building
    public class Industrial : Building
    {
        public static List<Industrial> all = new();
        int range;
        int impact;
        // Constructor for the Industrial class
        public Industrial(string name, char symbol, List<int> coordinates, int range, int impact) : base(name, symbol, coordinates)
        {
            this.range = range;
            this.impact = impact;
            all.Add(this);
        }

        // Method to find houses in the specified range on the map
        public List<House> FindHousesInRange(Map map)
        {
            List<House> housesInRange = new();
            // Iterate over possible rows in the range
            for (int possible_row= -this.range; possible_row<=this.range; possible_row++)
            {
                // Iterate over possible columns in the range
                for (int possible_col= -this.range; possible_col<=this.range; possible_col++)
                {
                    if(Math.Abs(possible_row) + Math.Abs(possible_col) <= this.range)
                    {
                        // Calculate the current coordinates based on the range
                        List<int> curr_coord = new List<int>{this.coordinates[0] + possible_col, this.coordinates[1] + possible_row};
                        // Check if the current coordinates are not the center (0, 0)
                        // and are within the bounds of the map
                        foreach(List<int> house_coord in House.all.Keys)
                        {
                            if(curr_coord[0] == house_coord[0] && curr_coord[1] == house_coord[1])
                            {
                                housesInRange.Add(House.all[house_coord]);
                                break;
                            }
                        }
                    }
                }
            }
            return housesInRange;
        }

        public void ImpactHouses(Map map)
        {
            foreach (House house in this.FindHousesInRange(map))
            {
                house.survivabilityIndex += this.impact;
            }
        }
    }
}