namespace WorldOfZuul
{
    public class Program
    {

        public class User
        {
            public List<int> currentSquare;
            public List<int> previousSquare;
            public User()
            {
                this.currentSquare = new() {2, 2};
                this.previousSquare = new();
            }

            public void Move(char direction)
            {
                switch (direction)
                {
                    case 'D':
                        if (currentSquare[1] + 1 != 10) // 10 = map max X
                            currentSquare[1] += 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'A':
                        if (this.currentSquare[1] != 0)
                            this.currentSquare[1] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'W':
                        if (this.currentSquare[0] != 0)
                            this.currentSquare[0] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'S':
                        if (this.currentSquare[0] + 1 != 10) // 10 = map max Y 
                            this.currentSquare[0] += 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                }
            }
            
            public void Move(char direction, int steps)
            {
                switch (direction)
                {
                    case 'D':
                        if (this.currentSquare[1] + steps < 10) // 10 = map max X
                            this.currentSquare[1] += steps;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'A':
                        if (this.currentSquare[1] - steps >= 0)
                            this.currentSquare[1] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'W':
                        if (this.currentSquare[0] - steps >= 0)
                            this.currentSquare[0] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case 'S':
                        if (this.currentSquare[0] + steps < 10) // 10 = map max Y 
                            this.currentSquare[0] += steps;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                }
            }          
        };

        public class Map
        {
            public static List<List<string>> map = new();
                
            public void Print(List<int> playerPosition)
            {
                Console.WriteLine();
                for(int i = playerPosition[0] - 2; i <= playerPosition[0] + 2; i++)
                {
                    for(int j = playerPosition[1] - 2; j <= playerPosition[1] + 2; j++)
                    {
                        if (i>=0 && j>=0 && i < map.Count && j < map[0].Count)
                        {
                            if (i == playerPosition[0] && j == playerPosition[1])
                                map[playerPosition[0]][playerPosition[1]] = "\uD83C\uDFE1"; // Player
                            Console.Write(map[i][j]);   
                        }
                        // Adding borders
                        else if (i<0)
                        {
                            Console.Write("_");
                        }
                        else if (j<0 || j>=map[0].Count)
                        {
                            Console.Write("|");
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public void PrintFull(List<int> playerPosition)
            {
                Console.WriteLine();
                for(int i=0; i < map.Count; i++)
                {
                    for(int j=0; j < map[i].Count; j++)
                    {
                        if (i == playerPosition[0] && j == playerPosition[1])
                                Console.Write("\uD83C\uDFE1"); // Player
                        else
                            Console.Write(map[i][j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public void Initialize(int hor, int ver)
            {
                for(int i=0; i<ver; i++)
                {
                    map.Add(new List<string>());
                    for(int j=0; j<hor; j++)
                    {
                        map[i].Add("\u2B1B");
                    }
                }
            }
            public void Change(List<int> squareToChange)
            {
                map[squareToChange[0]][squareToChange[1]] = "\uD83C\uDF3C"; // Squares, that the player has visited - later should pass in function what is in the square (house/tree/etc)
            }
        }

        class Square
        {
            public Square? Value { get; }
        }
        public static void Main()
        {
            bool running = true;
            Map map = new();
            running = true;
            User player = new();

            int xSize = 10;
            int ySize = 10;

            map.Initialize(ySize, xSize);
            map.PrintFull(player.currentSquare);

            while (running)
            {
                Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\nQ-Quit\n\nExtra Options:\n"); //instructions
                //if square is occupied - offer to use a shovel to clear the square
                //if square is not occupied - offer to place a building
                //if square is a tree - offer to cut down the tree
                //if square is the minesman - offer to ask for a hint
                string? userChoice = Console.ReadLine();
                player.previousSquare = new List<int>(player.currentSquare);
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                if (userChoice == "Q")
                {
                    running = false;
                }
                // else if (userChoice == "M")
                // {
                //     map.PrintFull(player.currentSquare);
                // }
                else if ("WASD".Contains(userChoice) && userChoice.Length == 1)
                {
                    char userChoice2 = char.Parse(userChoice);
                    player.Move(userChoice2);
                }
                else if (userChoice.Split().Length == 2 && "WASD".Contains(userChoice.Split()[0]))
                {
                    string[] split = userChoice.Split();
                    char direction = char.Parse(split[0]);
                    int steps = int.Parse(split[1]);
                    player.Move(direction, steps);
                }
                else
                {
                    Console.WriteLine("Error!");
                }
                if (running)
                {
                    map.Change(player.previousSquare);
                    map.PrintFull(player.currentSquare);
                    // map.PrintFull();
                }
            }
        }
    }
}
/// 