using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace WorldOfZuul
{
    public class Program
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
        };

        public class Map
        {
            public List<List<Square>> this_map = new(); //2d list => [[]]
                
            public void Initialize(int hor, int ver)  //2d list => [["f", ...], ...]
            {
                List<int> possible_sides = new() {0, hor};
                Random rnd = new Random();
                int mines_side_index = rnd.Next(0,2);
                int mines_row_index = rnd.Next(2,ver-3);
                List<int> mines_row_list = new() {mines_row_index-2, mines_row_index-1, mines_row_index, mines_row_index+1, mines_row_index+2};

                int central_tree_ver = rnd.Next(2,ver-3);
                int central_tree_hor = rnd.Next(2,hor-3);

                Console.WriteLine($"Central coords: ({central_tree_ver}, {central_tree_hor})");

                List<List<int>> possible_tree_coords = new();
                for (int possible_row=-1; possible_row<=1; possible_row++)
                {
                    for (int possible_col=-1; possible_col<=1; possible_col++)
                    {
                        List<int> curr_coord = new List<int>{central_tree_ver + possible_row, central_tree_hor + possible_col};
                        if (possible_row != 0 || possible_col != 0)
                        {
                            possible_tree_coords.Add(curr_coord);
                        }
                    }
                }

                List<List<int>> tree_coords = new();
                List<int> central_tree_coords = new(){central_tree_ver, central_tree_hor};
                tree_coords.Add(central_tree_coords);
                
                for (int i = 0; i <= 3; i++)
                {
                    int rndTreeIndex = rnd.Next(0, possible_tree_coords.Count);
                    List<int> rndTree = possible_tree_coords[rndTreeIndex];
                    tree_coords.Add(rndTree);
                    possible_tree_coords.RemoveAt(rndTreeIndex);
                }

                for(int row=0; row<ver; row++)
                {
                    this_map.Add(new List<Square>());
                    for(int column=0; column<hor; column++)
                    {
                        List<int> curr_coords = new() {row, column};
                        if(row == 0)
                        {
                            Square square = new('w'); //water
                            this_map[row].Add(square);
                        }
                        else if(row == (ver - 1))
                        {
                            Square square = new('t'); //tree
                            this_map[row].Add(square);
                        }
                        else if(mines_row_list.Contains(row) && column == possible_sides[mines_side_index])
                        {
                            Square square = new('m'); // mines
                            this_map[row].Add(square);
                        }
                        else
                        {
                            bool found_square = false;
                            for (int z=0; z<tree_coords.Count(); z++)
                            {
                                if (tree_coords[z][0] == row && tree_coords[z][1] == column)
                                {
                                    Square square = new('t'); // random trees
                                    this_map[row].Add(square);
                                    found_square = true;
                                    break;
                                }
                            }
                            if (!found_square)
                            {
                                Square square = new('p'); // plain
                                this_map[row].Add(square);
                            }
                        }
                    }
                }
            }

            public void PrintFull(List<int> playerPosition)
            {
                Console.WriteLine();
                for(int row=0; row < this_map.Count; row++)
                {
                    for(int column=0; column < this_map[row].Count; column++)
                    {
                        if (row == playerPosition[0] && column == playerPosition[1])
                                Console.Write("C"); // Player
                        else
                            Console.Write(this_map[row][column].value);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public void Change(Square squareToChange, char newValue)
            {
                squareToChange.changeValue(newValue);
            }
            public void Change(List<int> squareCoords, char newValue)
            {
                Square squareToChange = this_map[squareCoords[0]][squareCoords[1]];
                squareToChange.changeValue(newValue);
            }
        }

        public class Square
        {
            public char? value;

            public Square(char defValue)
            {
                value = defValue;
            }

            public void changeValue(char newValue)
            {
                value = newValue;
            }
        }
        public static void Main()
        {
            bool running = true;
            
            Map map = new();
            int xSize = 10; //10
            int ySize = 10;

            map.Initialize(ySize, xSize);

            User player = new(map);

            // Welcome to the game
            map.PrintFull(player.currentCoords);

            while (running)
            {
                Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\nQ-Quit\n\nExtra Options:\n"); //instructions
                //if square is occupied - offer to use a shovel to clear the square
                //if square is not occupied - offer to place a building
                //if square is a tree - offer to cut down the tree
                //if square is the minesman - offer to ask for a hint
                string? userChoice = Console.ReadLine().ToLower();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"); // for better visualization
                if (userChoice == "q")
                {
                    running = false;
                }
                // else if (userChoice == "m")
                // {
                //     map.PrintFull(player.currentSquare);
                // }
                else if ("wasd".Contains(userChoice) && userChoice.Length == 1)
                {
                    char userChoice2 = char.Parse(userChoice);
                    player.Move(userChoice2);
                }
                else if (userChoice.Split().Length == 2 && "wasd".Contains(userChoice.Split()[0]))
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
                    map.PrintFull(player.currentCoords);
                    Console.WriteLine($"User is standing on {player.currentSquare.value}");
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square 
/// VALIDATE IMPUT