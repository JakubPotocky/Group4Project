using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
        // Define a Building class
    public class Building
    {
        // Properties of a building
        public string name;
        public char symbol;
        protected List<int> coordinates;

        // Constructor for the Building class
        public Building (string name, char symbol, List<int> coordinates)
        {
            this.name = name;
            this.symbol = symbol;
            this.coordinates = coordinates;
        }
    }
    
    // Define a House class that inherits from Building
    public class House : Building
    {
        public static List<House> all = new();
        public int inhabitants;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex

        // Constructor for the House class
        public House(string name, List<int> coordinates, int humanCount, int survivabilityIndex) : base(name, 'l', coordinates)
        {
            this.inhabitants = humanCount;
            this.survivabilityIndex = survivabilityIndex;
            all.Add(this);
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
        public List<Square> FindHousesInRange(Map map)
        {
            List<Square> houseSquares = new();
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
                        if ((possible_row != 0 || possible_col != 0) && curr_coord[0] >= 0 && curr_coord[1] >= 0 && curr_coord[0] < map.this_map[0].Count && curr_coord[1] < map.this_map.Count)
                        {
                            // Access the square at the current coordinates
                            Square curr_square = map.this_map[curr_coord[1]][curr_coord[0]];
                            // Check if the square contains a house ('l')
                            if(curr_square.value == 'l')
                            {
                                // Add the square to the list of house squares
                                houseSquares.Add(curr_square);
                            }
                        }
                    }
                }
            }
            return houseSquares;
        }

        public void ImpactHouses(Map map)
        {
            foreach (Square house in this.FindHousesInRange(map))
            {
                if (house.obj is House)
                {
                    House humanHouse = house.obj as House;
                    humanHouse.survivabilityIndex += this.impact;
                    Console.WriteLine($"Current houses surv index: {humanHouse.survivabilityIndex}");
                }
            }
        }
    }
}