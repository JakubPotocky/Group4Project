namespace WorldOfZuul
{
    public class Program
    {

        public class User
        {
            public List<int> currentSquare = new() {2, 2};
            public List<int> previousSquare = new();
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
                                Console.Write("O"); // Player
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
            public Square? Left { get; }
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
            map.Print(player.currentSquare);

            while (running)
            {
                Console.WriteLine("\nWhich direction do you want to go?\nR-Right\nU-Up\nL-Left\nD-Down\nQ-Quit\n\nExtra Options:\nM-Show full map\n");
                string? userChoice = Console.ReadLine();
                player.previousSquare = new List<int>(player.currentSquare);
                switch (userChoice) // Probably should be player.Move(userChoice) -> Move method also assigns the current square to the variable previous square and deals with map changes
                {
                    case "R":
                        if (player.currentSquare[1] + 1 != xSize)
                            player.currentSquare[1] += 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case "L":
                        if (player.currentSquare[1] != 0)
                            player.currentSquare[1] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case "U":
                        if (player.currentSquare[0] != 0)
                            player.currentSquare[0] -= 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case "D":
                        if (player.currentSquare[0] + 1 != ySize)
                            player.currentSquare[0] += 1;
                        else
                            Console.WriteLine("You can't go there!");
                        break;
                    case "M":
                        map.PrintFull(player.currentSquare);
                        break;
                    case "Q":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                if (running)
                {

                    // For debugging
                    // Console.WriteLine(player.previousSquare[0] + " " + player.previousSquare[1] + " " + player.currentSquare[0] + " " + player.currentSquare[1]);

                    map.Change(player.previousSquare);
                    map.Print(player.currentSquare);
                    // map.PrintFull();
                }
            }
        }
    }
}