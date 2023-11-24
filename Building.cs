using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
    public class Building
    {
        public string name;
        public char symbol;
        public int number;
        public List<int> resources;
        public Building (string name, char symbol, int number, List<int> resources)
        {
            this.name = name;
            this.symbol = symbol;
            this.number = number;
            this.resources = resources;
        }
    }
    
    public class House : Building
    {
        int humanCount;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex
        public House(string name, int number, List<int> resources, int humanCount, int survivabilityIndex) : base(name, 'l', number, resources)
        {
            this.humanCount = humanCount;
            this.survivabilityIndex = survivabilityIndex;
        }
    }

    public class Industrial : Building
    {
        int range;
        int impact;
        List<int>? coordinates;
        //int workingPeople;

        public Industrial(string name, char symbol, int number, List<int> resources, int range, int impact, List<int>? coordinates/*, int workingPeople|*/) : base(name, symbol, number, resources)
        {
            this.range = range;
            this.impact = impact;
            this.coordinates = coordinates;
            // this.workingPeople = workingPeople; 
        }
        public List<Square> FindHousesInRange(Map map)
        {
            List<Square> houseSquares = new();

            for (int possible_row = -this.range; possible_row<=this.range; possible_row++)
            {
                for (int possible_col= - this.range; possible_col<=this.range; possible_col++)
                {
                    List<int> curr_coord = new List<int>{this.coordinates[1] + possible_row, this.coordinates[0] + possible_col};
                    if ((possible_row != 0 || possible_col != 0) && curr_coord[0] >= 0 && curr_coord[1] >= 0 && curr_coord[0] <= map.this_map[0].Count && curr_coord[1] <= map.this_map.Count)
                    {
                        Square curr_square = map.this_map[curr_coord[0]][curr_coord[1]];
                        if(curr_square.value == 'h') //later chage this to the symbol for houses
                            houseSquares.Add(curr_square);
                    }
                }
            }

            return houseSquares;
        }

        // public void ImpactBuildings (Map map)
        // {
        //     foreach (Square house in FindHousesInRange(map))
        //     {
        //         house.survivabilityIndex += this.impact;
        //     }
        // }
    }
}