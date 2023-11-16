using WorldOfZuul;

namespace WorldOfZuul
{
    public class User
    {
        public List<int> currentCoords; //coordinates (x, y)
        public Square? currentSquare; //the actual current square - the Square OBJECT
        public Map map;

        public User(Map map)
        {
            currentCoords = new() {2, 2};
            currentSquare = map.this_map[2][2];
            this.map = map;
        }
        
        public void ChangeCoords(int x, int y)
        {
            currentCoords[0] += y;
            currentCoords[1] += x;
            currentSquare = map.this_map[currentCoords[0]][currentCoords[1]];
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'd':
                    if (currentCoords[1] + 1 != map.this_map[1].Count) // 10 = map max X
                        ChangeCoords(1, 0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'a':
                    if (currentCoords[1] != 0)
                        ChangeCoords(-1, 0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'w':
                    if (currentCoords[0] != 0)
                        ChangeCoords(0, -1);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 's':
                    if (currentCoords[0] + 1 != map.this_map.Count) // 10 = map max Y 
                        ChangeCoords(0, 1);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
            }
        }
        
        public void Move(char direction, int steps)
        {
            switch (direction)
            {
                case 'd':
                    if (currentCoords[1] + steps < map.this_map[1].Count) // 10 = map max X
                        ChangeCoords(steps,0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'a':
                    if (currentCoords[1] - steps >= 0)
                        ChangeCoords(-steps,0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'w':
                    if (currentCoords[0] - steps >= 0)
                        ChangeCoords(0,-steps);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 's':
                    if (currentCoords[0] + steps < map.this_map.Count) // 10 = map max Y 
                        ChangeCoords(0, steps);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
            }
        }
    }
}