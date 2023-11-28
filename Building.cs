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
        public int number;
        public List<int> resources;

        // Constructor for the Building class
        public Building (string name, char symbol, int number, List<int> resources)
        {
            this.name = name;
            this.symbol = symbol;
            this.number = number;
            this.resources = resources;
        }
    }
    
    // Define a House class that inherits from Building
    public class House : Building
    {
        int humanCount;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex

        // Constructor for the House class
        public House(string name, int number, List<int> resources, int humanCount, int survivabilityIndex) : base(name, 'l', number, resources)
        {
            this.humanCount = humanCount;
            this.survivabilityIndex = survivabilityIndex;
        }
    }

    // Define an Industrial class that inherits from Building
    public class Industrial : Building
    {
        int range;
        int impact;
        List<int>? coordinates;
        //int workingPeople;

        // Constructor for the Industrial class
        public Industrial(string name, char symbol, int number, List<int> resources, int range, int impact, List<int>? coordinates/*, int workingPeople|*/) : base(name, symbol, number, resources)
        {
            this.range = range;
            this.impact = impact;
            this.coordinates = coordinates;
            // this.workingPeople = workingPeople; 
        }

        // Method to find houses in the specified range on the map
        public List<Square> FindHousesInRange(Map map)
        {
            List<Square> houseSquares = new();

            // Iterate over possible rows in the range
            for (int possible_row = -this.range; possible_row<=this.range; possible_row++)
            {
                // Iterate over possible columns in the rang
                for (int possible_col= - this.range; possible_col<=this.range; possible_col++)
                {
                    // Calculate the current coordinates based on the range
                    List<int> curr_coord = new List<int>{this.coordinates[1] + possible_row, this.coordinates[0] + possible_col};

                    // Check if the current coordinates are not the center (0, 0)
                    // and are within the bounds of the map
                    if ((possible_row != 0 || possible_col != 0) && curr_coord[0] >= 0 && curr_coord[1] >= 0 && curr_coord[0] <= map.this_map[0].Count && curr_coord[1] <= map.this_map.Count)
                    {
                        // Access the square at the current coordinates
                        Square curr_square = map.this_map[curr_coord[0]][curr_coord[1]];

                        // Check if the square contains a house ('l')
                        if(curr_square.value == 'l')
                            // Add the square to the list of house squares
                            houseSquares.Add(curr_square);
                    }
                }
            }

            return houseSquares;
        }

    // Method to impact buildings in the specified range on the map
    public void ImpactBuildings (Map map)
        {
            foreach (Square house in FindHousesInRange(map))
            {
                if (house.obj is House && house.obj != null)
                {
                    House humanHouse = house.obj as House;
                    humanHouse.survivabilityIndex += this.impact;
                }
            }
        }
    }
}