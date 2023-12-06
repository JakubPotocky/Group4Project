using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
        // Define a Building class
    public class Building
    {
        // Properties of a building
        public static List<Building> all = new();
        public string name;
        public char symbol;
        public int number;
        public List<int> resources;

        // Constructor for the Building class
        public Building (string name, char symbol, int number, List<int> resources)
        {
            this.name = name;
            this.symbol = symbol;
            this.number = number;
            this.resources = resources;
            all.Add(this);
        }
    }
    
    // Define a House class that inherits from Building
    public class House : Building
    {
        public int inhabitants;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex

        // Constructor for the House class
        public House(string name, int number, List<int> resources, int humanCount, int survivabilityIndex) : base(name, 'l', number, resources)
        {
            this.inhabitants = humanCount;
            this.survivabilityIndex = survivabilityIndex;
        }
    }

    // Define an Industrial class that inherits from Building
    public class Industrial : Building
    {
        int range;
        int impact;
        public List<int>? coordinates;
        // Constructor for the Industrial class
        public Industrial(string name, char symbol, int number, List<int> resources, int range, int impact, List<int>? coordinates) : base(name, symbol, number, resources)
        {
            this.range = range;
            this.impact = impact;
            this.coordinates = coordinates;
        }

        // Method to find houses in the specified range on the map
        public List<Square> FindHousesInRange(Map map)
        {
            List<Square> houseSquares = new();

            Console.WriteLine($"Building coordinates: {this.coordinates[0]}, {this.coordinates[1]}");
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
                            if (this.symbol == 'm')
                            {
                                Console.WriteLine(curr_coord[0]);
                                Console.WriteLine(curr_coord[1]);
                                Console.WriteLine(curr_square.value);
                            }
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

            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.WriteLine(this.symbol);
            Console.WriteLine(houseSquares.Count);
            return houseSquares;
        }

        public void ImpactBuildings (Map map)
            {
                foreach (Square house in this.FindHousesInRange(map))
                {
                    Console.WriteLine(house.value);
                    if (house.obj is House && house.obj != null)
                    {
                        House humanHouse = house.obj as House;
                        humanHouse.survivabilityIndex += this.impact;
                    }
                }
            }
        }
}