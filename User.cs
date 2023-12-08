using WorldOfZuul;

namespace WorldOfZuul
{
    public class User
    {
        // Represents the current coordinates by (x, y) of the user on the map
        public List<int> currentCoords = new() {2, 2}; 
        public Square currentSquare; //the actual current square - the Square OBJECT
        public Map map; //reference to the game map
        public int hintsLeft = 5; //number of avaliable hints
        public int shovelsLeft = 5;
        public Blueprint? currentBlueprint; //what object is the user currently interacting with
        public int wood=1000; // resources held by the user
        public int stone=1000;

        public User(Map map)
        {
            currentSquare = map.this_map[2][2]; //starting position on the map
            this.map = map;
        }
        
        public void ChangeCoords(int x, int y) //updating user's location
        {
            currentCoords[0] += x;
            currentCoords[1] += y;
            currentSquare = map.this_map[currentCoords[1]][currentCoords[0]];
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'd':
                    if (currentCoords[0] + 1 != map.this_map[0].Count) // 10 = map max X //if within map boundaries
                        ChangeCoords(1, 0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'a':
                    if (currentCoords[0] != 0)
                        ChangeCoords(-1, 0);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 'w':
                    if (currentCoords[1] != 0)
                        ChangeCoords(0, -1);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
                case 's':
                    if (currentCoords[1] + 1 != map.this_map.Count) // 10 = map max Y //if within map boundaries
                        ChangeCoords(0, 1);
                    else
                        Console.WriteLine("You can't go there!");
                    break;
            }
        }
        
        public void Move(char direction, int steps) //User movement control
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