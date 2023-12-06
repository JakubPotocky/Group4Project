using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
    public class Building
    {
        public static List<Building> all = new();
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
            all.Add(this);
        }
    }
    
    public class House : Building
    {
        public int inhabitants;
        public int survivabilityIndex; //The chances of user dying are 1/survivabilityIndex
        public House(string name, int number, List<int> resources, int humanCount, int survivabilityIndex) : base(name, 'l', number, resources)
        {
            this.inhabitants = humanCount;
            this.survivabilityIndex = survivabilityIndex;
        }
    }

    public class Industrial : Building
    {
        int range;
        int impact;
        public List<int>? coordinates;
        public Industrial(string name, char symbol, int number, List<int> resources, int range, int impact, List<int>? coordinates) : base(name, symbol, number, resources)
        {
            this.range = range;
            this.impact = impact;
            this.coordinates = coordinates;
        }
        public List<Square> FindHousesInRange(Map map)
        {
            List<Square> houseSquares = new();

            Console.WriteLine($"Building coordinates: {this.coordinates[0]}, {this.coordinates[1]}");
            for (int possible_row= -this.range; possible_row<=this.range; possible_row++)
            {
                for (int possible_col= -this.range; possible_col<=this.range; possible_col++)
                {
                    if(Math.Abs(possible_row) + Math.Abs(possible_col) <= this.range)
                    {
                        List<int> curr_coord = new List<int>{this.coordinates[0] + possible_col, this.coordinates[1] + possible_row};
                        if ((possible_row != 0 || possible_col != 0) && curr_coord[0] >= 0 && curr_coord[1] >= 0 && curr_coord[0] < map.this_map[0].Count && curr_coord[1] < map.this_map.Count)
                        {
                            Square curr_square = map.this_map[curr_coord[1]][curr_coord[0]];
                            if (this.symbol == 'm')
                            {
                                Console.WriteLine(curr_coord[0]);
                                Console.WriteLine(curr_coord[1]);
                                Console.WriteLine(curr_square.value);
                            }
                            if(curr_square.value == 'l')
                            {
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