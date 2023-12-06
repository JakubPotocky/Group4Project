using System.ComponentModel;
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
        public static int stepCount = 0;
        public static int stepAmount = 20;
        public static int buildingCount = 0;
        public static bool running = true;
        public static void Main()
        {
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
            Console.WriteLine("Welcome to EcoCity: Building a Sustainable Future! Your goal is to make the city as sustainable as possible. Start by exploring the map and finding the last city mayor. Good luck!");
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
                        Console.WriteLine("Last City Mayor:"); 
                        Console.WriteLine(Program.Mayor.GetPrompt("Introduction"));
                        Console.WriteLine();
                        Console.WriteLine(Program.Mayor.GetPrompt("Quest1"));
                        Quests.StartQuest(1, player);
                        player.currentSquare.value = '♦';
                        Program.mayorStart = true;
                    }
                    if (!minerStart && player.currentSquare.value == '∆') //Introduce the minor after meeting the mayor
                    {
                        if (!Program.minerStart && Program.mayorStart)
                        {
                            Console.WriteLine("Miner:");
                            Console.WriteLine(Program.Miner.GetPrompt("Introduction"));
                            Program.minerStart = true;
                        }
                    }
                }
                else if(userChoice == ConsoleKey.L) //Legend
                {
                    Functions.PrintMapLegend();
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
                else if(minerStart && userChoice == ConsoleKey.H && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                {
                    player.hintsLeft--;
                    Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                    Console.WriteLine($" You have {player.hintsLeft} hints left!");
                }
                else if(minerStart && userChoice == ConsoleKey.H && player.hintsLeft==0 && player.currentSquare.value == '∆')
                {
                    Console.WriteLine(Miner.GetPrompt("Exceed"));
                }
                else if(mayorStart)
                {
                    if(buildingCount<player.currentBuilding.number && userChoice == ConsoleKey.B && player.currentBuilding != null && player.currentSquare.value == '♦')
                    {
                        if (player.wood >= player.currentBuilding.resources[0] && player.stone >= player.currentBuilding.resources[1])
                        {
                            player.currentSquare.value = player.currentBuilding.symbol;
                            player.wood -= player.currentBuilding.resources[0];
                            player.stone -= player.currentBuilding.resources[1];
                            if(player.currentBuilding is Industrial)
                            {
                                Industrial industrial = player.currentBuilding as Industrial;
                                industrial.coordinates = player.currentCoords;
                            }
                            buildingCount++;
                        }
                        else
                        {
                            Console.WriteLine("Not enough resources!");
                        }
                        if (buildingCount==player.currentBuilding.number) 
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
                        Console.WriteLine($"Progress: {stepCount*100/stepAmount}%");
                        if (player.currentBuilding.number == 1)
                        {
                            Console.WriteLine($"Current quest: {Quests.Prompts[$"Quest{stepCount+1}"]}");
                        }
                        else
                        {
                            Console.WriteLine($"Current quest: {Quests.Prompts[$"Quest{stepCount+1}"]}({buildingCount}/{player.currentBuilding.number})");
                        }
                    }
                    if (player.currentSquare.value != null)
                    {
                        Functions.PrintUserOptions(player);
                        Console.WriteLine($"\nWood: {player.wood} \nStone: {player.stone}");
                    }
                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 