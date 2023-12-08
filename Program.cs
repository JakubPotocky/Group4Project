using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace WorldOfZuul
{
    public class Program
    {
        // Define non-player characters (NPSs) and game variables
        public static NPC Mayor = new("Mayor", MayorPrompts.Prompts);
        public static NPC Miner = new("Miner", MinerPrompts.Prompts);
        public static bool mayorStart = false;
        public static bool minerStart = false;
        public static int Hintcounter = 0;
        public static int stepCount = 0;
        public static int stepAmount = 20;
        public static int buildingCount = 0;
        public static bool running = true;
        public static void Main()
        {
            List<int> test1 = new List<int>();
            List<int> test2 = new List<int>(); 

            //Define resources
            int plusWood = 5; // *5 for delete
            int plusStone = 5;

            //string[] NPCprompts = File.ReadAllLines("NPCprompts/");
            //Create map game and choose its size
            Map map = new();
            int xSize = 10; //10
            int ySize = 10;
            
            map.Initialize(ySize, xSize);
            // Character creation
            User player = new(map);
            // Welcome message to the game
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");// for better visualization
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to EcoCity: Building a Sustainable Future! Your goal is to make the city as sustainable as possible. Start by exploring the map and finding the last city mayor. Good luck!");
            Console.ResetColor();
            Functions.PrintUserOptions(player);
            map.Print(player.currentCoords);
            
            ConsoleKeyInfo keyInfo;
            //Main game loop
            while (running)
            {
                keyInfo = Console.ReadKey(); //Read user input
                //create a new funciton that checks what square is player standing on and gives him choices to make
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"); // for better visualization

                //instructions
                //if square is occupied - offer to use a shovel to clear the square
                //if square is not occupied - offer to place a building
                //if square is a tree - offer to cut down the tree
                //if square is the minesman - offer to ask for a hint
                
                ConsoleKey? userChoice = keyInfo.Key;
                if (userChoice == null)
                {
                    Console.WriteLine("Error! The application crashed!");
                    continue;
                }
                else if (userChoice == ConsoleKey.Q) //Possibility to quit the game
                {
                    running = false;
                }

                //Player control options (up, down, left, right inside the game)

                else if (userChoice == ConsoleKey.W || userChoice == ConsoleKey.A || userChoice == ConsoleKey.S || userChoice == ConsoleKey.D || userChoice == ConsoleKey.UpArrow || userChoice == ConsoleKey.DownArrow || userChoice == ConsoleKey.LeftArrow || userChoice == ConsoleKey.RightArrow) //Movement
                {
                    if(userChoice == ConsoleKey.W || userChoice == ConsoleKey.UpArrow)
                    {player.Move('w');}
                    if(userChoice == ConsoleKey.A || userChoice == ConsoleKey.LeftArrow)
                    {player.Move('a');}
                    if(userChoice == ConsoleKey.S || userChoice == ConsoleKey.DownArrow)
                    {player.Move('s');}
                    if(userChoice == ConsoleKey.D || userChoice == ConsoleKey.RightArrow)
                    {player.Move('d');}
                    
                    if(!mayorStart && player.currentSquare.value == 'M') //introduce tha mayor and start the quest
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Last City Mayor:"); 
                        Console.ResetColor();
                        Console.WriteLine(Mayor.GetPrompt("Introduction"));
                        Console.WriteLine();
                        Console.WriteLine(Mayor.GetPrompt("Quest1"));
                        Quests.StartQuest(1, player);
                        player.currentSquare.value = '♦';
                        mayorStart = true;
                    }

                    if (!minerStart && player.currentSquare.value == '∆') //Introduce the minor after meeting the mayor
                    {
                        if (!minerStart && mayorStart)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Miner:");
                            Console.ResetColor();
                            Console.WriteLine(Miner.GetPrompt("Introduction"));
                            minerStart = true;
                        }
                    }
                }
                else if(userChoice == ConsoleKey.L) //Legend
                {
                    Functions.PrintMapLegend();
                }
                else if(mayorStart && userChoice == ConsoleKey.M && player.currentSquare.value == '∆')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Last City Mayor:"); 
                    Console.ResetColor();
                    Console.WriteLine(Mayor.GetPrompt($"Quest{Program.stepCount+1}"));
                }
                else if(userChoice == ConsoleKey.X && player.currentSquare.value == '♧') //cutting trees
                {
                    player.wood += plusWood;
                }
                else if(userChoice == ConsoleKey.P && player.currentSquare.value == '♧') //cutting trees permanently 
                {
                    player.currentSquare.value = '♦';
                    player.wood += plusWood*10;
                }
                // else if(userChoice == "t" && player.currentSquare.value == '♦') //planting new trees 
                // {
                //     player.currentSquare.value = '♧';
                // }
                else if(userChoice == ConsoleKey.X && player.currentSquare.value == '∆') //mining stone
                {
                    player.stone += plusStone;
                }
                else if((Hintcounter!=stepCount+1 || Hintcounter==0) && minerStart && userChoice == ConsoleKey.H && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                {
                    Hintcounter=stepCount+1;
                    player.hintsLeft--;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Miner:");
                    Console.ResetColor();
                    Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                    Console.WriteLine($" You have {player.hintsLeft} hints left!");
                }
                else if(Hintcounter==stepCount+1 && minerStart && userChoice == ConsoleKey.H && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Miner:");
                    Console.ResetColor();
                    Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                    Console.WriteLine($" You have {player.hintsLeft} hints left!");
                }
                else if(minerStart && userChoice == ConsoleKey.H && player.hintsLeft==0 && player.currentSquare.value == '∆')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Miner:");
                    Console.ResetColor();
                    Console.WriteLine(Miner.GetPrompt("Exceed"));
                }
                else if(mayorStart)
                {
                    if(buildingCount<player.currentBlueprint.count && userChoice == ConsoleKey.B && player.currentSquare.value == '♦')
                    {
                        if (player.wood >= player.currentBlueprint.resources[0] && player.stone >= player.currentBlueprint.resources[1])
                        {
                            player.currentSquare.value = player.currentBlueprint.symbol;
                            player.currentSquare.obj = player.currentBlueprint.Build(player.currentCoords);
                            player.wood -= player.currentBlueprint.resources[0];
                            player.stone -= player.currentBlueprint.resources[1];
                            buildingCount++;
                        }
                        else
                        {
                            Console.WriteLine("Not enough resources!");
                        }
                        if (buildingCount==player.currentBlueprint.count) 
                        {
                            Quests.CompleteQuest(map, player, Mayor, running);
                            buildingCount=0;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error! Incorrect input!");
                }
                if (running && stepCount<stepAmount)
                { //Display map and game information
                    if(mayorStart)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\nProgress: ");
                        Console.ResetColor();
                        Console.Write($"{stepCount*100/stepAmount}%\n");
                        if (player.currentBlueprint.count == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\nCurrent quest: ");
                            Console.ResetColor();
                            Console.Write($"{Quests.Prompts[$"Quest{stepCount+1}"]}\n");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\nCurrent quest: ");
                            Console.ResetColor();
                            Console.Write($"{Quests.Prompts[$"Quest{stepCount+1}"]}({buildingCount}/{player.currentBlueprint.count})\n");
                        }
                    }
                    if (player.currentSquare.value != null)
                    {
                        Functions.PrintUserOptions(player);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n\nWood: ");
                        Console.ResetColor();
                        Console.Write($"{player.wood}");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nStone: ");
                        Console.ResetColor();
                        Console.Write($"{player.stone}\n");
                    }
                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 