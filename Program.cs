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
        public static Dwarf dwarf = new("Dwarf", DwarfPrompts.Prompts);
        public static Captain captain = new("Captain", CaptainPrompts.Prompts);
        public static bool mayorStart = false;
        public static bool minerStart = false;
        public static bool dwarfStart = false;
        public static bool captainStart = false;
        public static int hintCounter = 0;
        public static int stepCount = 0;
        public static int stepAmount = 20;
        public static int buildingCount = 0;
        public static bool running = true;
        public static List<List<int>> regrowingTrees = new();
        public static List<List<int>> regeneratingMines = new();
        public static void Main()
        {

            //Define resources
            int plusWood = 5;
            int plusStone = 10;

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
                ConsoleKey? userChoice = keyInfo.Key;
                if(dwarf.buildingInShovel != null && userChoice != ConsoleKey.G)
                {
                    Console.WriteLine($"The dwarf has a {dwarf.buildingInShovel.name} in shovel and is waiting for you to give him instruction to place it!");
                }
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
                        Quests.StartQuest(1, player, Mayor);
                        player.currentSquare.value = '♦';
                        mayorStart = true;
                    }

                    else if (!minerStart && player.currentSquare.value == '∆') //Introduce the minor after meeting the mayor
                    {
                        if (mayorStart)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("John the Miner:");
                            Console.ResetColor();
                            Console.WriteLine(Miner.GetPrompt("Introduction"));
                            minerStart = true;
                        }
                    }

                    else if(!dwarfStart && player.currentSquare.value == '♧' && player.currentCoords[1] == ySize-1)
                    {
                        if (mayorStart)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("The Ugly Dwarf:");
                            Console.ResetColor();
                            Console.WriteLine(dwarf.GetPrompt("Introduction"));
                            dwarfStart = true;
                        }
                    }
                    else if(!captainStart && player.currentSquare.value == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Captain:");
                        Console.ResetColor();
                        Console.WriteLine(captain.GetPrompt("Introduction"));
                        captainStart = true;
                    }
                    else if(player.currentSquare.value == 'C')
                    {
                        if(player.currentBlueprint is IndustrialBlueprint && player.extraResource == null)
                        {
                            IndustrialBlueprint industrialBlueprint = player.currentBlueprint as IndustrialBlueprint;
                            if(industrialBlueprint.extraResource != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("The Captain:");
                                Console.ResetColor();
                                Console.WriteLine(captain.GetPrompt($"Quest{stepCount+1}"));
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("The Captain:");
                            Console.ResetColor();
                            Console.WriteLine(captain.GetPrompt($"Other"));
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
                    Console.WriteLine(Mayor.GetPrompt($"Quest{stepCount+1}"));
                }
                else if(dwarf.buildingInShovel == null)
                {
                    if(userChoice == ConsoleKey.X && player.currentSquare.value == '♧' && !Functions.ContainsEqualCoordinates(regrowingTrees, player.currentCoords)) //cutting trees
                    {
                        player.wood += plusWood;
                        regrowingTrees.Add(new(player.currentCoords));
                    }
                    else if(userChoice == ConsoleKey.P && player.currentSquare.value == '♧' && player.currentCoords[1] != ySize - 1&& !Functions.ContainsEqualCoordinates(regrowingTrees, player.currentCoords)) //cutting trees permanently 
                    {
                        player.currentSquare.value = '♦';
                        foreach(List<int> tree in map.tree_coords)
                        {
                            if (Functions.EqualCoordinates(tree, player.currentCoords))
                            {
                                map.tree_coords.Remove(tree);
                                break;
                            }
                        }
                        player.wood += plusWood*10;
                    }
                    else if ((userChoice == ConsoleKey.X || userChoice == ConsoleKey.P) && player.currentSquare.value == '♧')
                    {
                        Console.WriteLine("This tree is currently regrowing and cannot be cut down!");
                    }
                    else if(userChoice == ConsoleKey.X && player.currentSquare.value == '∆' && !Functions.ContainsEqualCoordinates(regeneratingMines, player.currentCoords)) //mining stone
                    {
                        player.stone += plusStone;
                        regeneratingMines.Add(new(player.currentCoords));
                    }
                    else if(userChoice == ConsoleKey.X && player.currentSquare.value == '∆')
                    {
                        Console.WriteLine("The mines are currently regenerating and cannot be mined!");
                    }
                    else if((hintCounter!=stepCount+1 || hintCounter==0) && minerStart && userChoice == ConsoleKey.H && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                    {
                        hintCounter=stepCount+1;
                        player.hintsLeft--;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("John the Miner:");
                        Console.ResetColor();
                        Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                        Console.WriteLine($" You have {player.hintsLeft} hints left!");
                    }
                    else if(hintCounter==stepCount+1 && minerStart && userChoice == ConsoleKey.H && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("John the Miner:");
                        Console.ResetColor();
                        Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                        Console.WriteLine($" You have {player.hintsLeft} hints left!");
                    }
                    else if(minerStart && userChoice == ConsoleKey.H && player.hintsLeft==0 && player.currentSquare.value == '∆')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("John the Miner:");
                        Console.ResetColor();
                        Console.WriteLine(Miner.GetPrompt("Exceed"));
                    }
                    else if (userChoice == ConsoleKey.G && player.currentSquare.obj != null && player.shovelsLeft != 0 && dwarfStart)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("The Ugly Dwarf:");
                        Console.ResetColor();
                        Console.WriteLine(dwarf.GetPrompt($"Error{6-player.shovelsLeft}"));
                        dwarf.buildingInShovel = player.currentSquare.obj;
                    }
                    else if(mayorStart)
                    {
                        if(buildingCount<player.currentBlueprint.count && userChoice == ConsoleKey.B && player.currentSquare.value == '♦')
                        {
                            if (player.currentBlueprint is IndustrialBlueprint)
                            {
                                IndustrialBlueprint industrialBlueprint = player.currentBlueprint as IndustrialBlueprint;
                                if (industrialBlueprint.extraResource == null)
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
                                }
                                else
                                {
                                    if (player.wood >= player.currentBlueprint.resources[0] && player.stone >= player.currentBlueprint.resources[1] && player.extraResource == industrialBlueprint.extraResource)
                                    {
                                        player.currentSquare.value = player.currentBlueprint.symbol;
                                        player.currentSquare.obj = player.currentBlueprint.Build(player.currentCoords);
                                        player.wood -= player.currentBlueprint.resources[0];
                                        player.stone -= player.currentBlueprint.resources[1];
                                        buildingCount++;
                                        player.extraResource = null;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough resources!");
                                    }
                                }
                            }
                            else
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
                            }
                            if (buildingCount==player.currentBlueprint.count) 
                            {
                                Quests.CompleteQuest(map, player, Mayor, running);
                                buildingCount=0;
                            }
                        }
                        else if(userChoice == ConsoleKey.T && player.currentSquare.value == 'C' && player.currentBlueprint is IndustrialBlueprint && player.extraResource == null)
                        {
                            IndustrialBlueprint industrialBlueprint = player.currentBlueprint as IndustrialBlueprint;
                            if (industrialBlueprint.extraResource != null && player.wood >= captain.cost[$"Quest{stepCount+1}"][0] && player.stone >= captain.cost[$"Quest{stepCount+1}"][1])
                            {
                                player.wood -= captain.cost[$"Quest{stepCount+1}"][0];
                                player.stone -= captain.cost[$"Quest{stepCount+1}"][1];
                                player.extraResource = industrialBlueprint.extraResource;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("The Captain:");
                                Console.ResetColor();
                                Console.WriteLine($"Here is the {player.extraResource}! Pleasure doing business with you!");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("The Captain:");
                                Console.ResetColor();
                                Console.WriteLine("I'm sorry buddy! You don't have enough resources! No deal!");
                            }
                        }
                    }
                }
                else if (userChoice == ConsoleKey.G && player.shovelsLeft != 0 && dwarfStart)
                {
                    Building currentBuilding = player.currentSquare.obj;
                    player.currentSquare.obj = dwarf.buildingInShovel;
                    player.currentSquare.value = dwarf.buildingInShovel.symbol;
                    List<int> previousCoords = new(dwarf.buildingInShovel.coordinates);
                    map.this_map[previousCoords[1]][previousCoords[0]].obj = currentBuilding;
                    map.this_map[previousCoords[1]][previousCoords[0]].value = currentBuilding.symbol;
                    currentBuilding.coordinates = new(dwarf.buildingInShovel.coordinates);
                    dwarf.buildingInShovel.coordinates = new(player.currentCoords);
                    dwarf.buildingInShovel = null;
                    player.shovelsLeft--;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("The Ugly Dwarf:");
                    Console.ResetColor();
                    Console.WriteLine($"There you go buddy! You have {player.shovelsLeft} shovels left!");
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
                        Console.Write($"{player.stone}");
                        if (player.extraResource != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"\n{player.extraResource}: ");
                            Console.ResetColor();
                            Console.Write("1");
                        }
                    }
                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 