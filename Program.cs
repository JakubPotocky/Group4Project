using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace WorldOfZuul
{
    public class Program
    {
        public static NPC Mayor = new("Mayor", MayorPrompts.Prompts);
        public static NPC Miner = new("Miner", MinerPrompts.Prompts);
        public static bool mayorStart = false;
        public static bool minerStart = false;
        public static int stepCount = 0;
        public static int stepAmount = 20;
        public static int buildingCount = 1;
        public static bool running = true;
        public static void Main()
        {
            int plusWood = 5; // *5 for delete
            int plusStone = 5;

            //string[] NPCprompts = File.ReadAllLines("NPCprompts/");
            
            Map map = new();
            int xSize = 10; //10
            int ySize = 10;
            
            map.Initialize(ySize, xSize);

            User player = new(map);
            // Welcome to the game
            Console.WriteLine("Welcome to EcoCity: Building a Sustainable Future! Your goal is to make the city as sustainable as possible. Start by exploring the map and finding the last city mayor. Good luck!");
            map.Print(player.currentCoords);

            while (running)
            {
                //create a new funciton that checks what square is player standing on and gives him choices to make
                if (player.currentSquare.value != null)
                {
                    Functions.PrintUserOptions(player);
                    Console.WriteLine($"\nWood: {player.wood} \nStone: {player.stone}");
                }

                //instructions
                //if square is occupied - offer to use a shovel to clear the square
                //if square is not occupied - offer to place a building
                //if square is a tree - offer to cut down the tree
                //if square is the minesman - offer to ask for a hint
                string? userChoice = Console.ReadLine();
                if (userChoice == null)
                {
                    Console.WriteLine("Error! The application crashed!");
                    continue;
                }
                else
                {
                    userChoice = userChoice.ToLower();
                }

                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n"); // for better visualization
        
                if (userChoice == "q") //Quit Game
                {
                    running = false;
                }
                else if ("wasd".Contains(userChoice) && userChoice.Length == 1) //Movement
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
                //else if(mayorStart && userChoice == "c") //Quests/Steps
                //{
                //  Quests.CompleteQuest(map, player, Mayor, running);
                //}
                else if(userChoice == "l") //Legend
                {
                    Functions.PrintMapLegend();
                }
                else if(userChoice == "x" && player.currentSquare.value == '♧') //cutting trees
                {
                    player.wood += plusWood;
                }
                else if(userChoice == "p" && player.currentSquare.value == '♧') //cutting trees permanently 
                {
                    player.currentSquare.value = '♦';
                    player.wood += plusWood*10;
                }
                // else if(userChoice == "t" && player.currentSquare.value == '♦') //planting new trees 
                // {
                //     player.currentSquare.value = '♧';
                // }
                else if(userChoice == "x" && player.currentSquare.value == '∆') //mining stone
                {
                    player.stone += plusStone;
                }
                else if(minerStart && userChoice == "h" && player.hintsLeft>0 && mayorStart && player.currentSquare.value == '∆') //Hints
                {
                    player.hintsLeft--;
                    Console.Write(Miner.GetPrompt($"Quest{stepCount+1}"));
                    Console.WriteLine($" You have {player.hintsLeft} hints left!");
                }
                else if(minerStart && userChoice == "h" && player.hintsLeft==0 && player.currentSquare.value == '∆')
                    Console.WriteLine(Miner.GetPrompt("Exceed"));
                else if(mayorStart && userChoice == "b" && player.currentBuilding != null && player.currentSquare.value == '♦')
                {
                    if (buildingCount<=player.currentBuilding.number)
                    {
                        if (player.wood >= player.currentBuilding.resources[0] && player.stone >= player.currentBuilding.resources[1])
                        {
                            player.currentSquare.value = player.currentBuilding.symbol;
                            player.wood -= player.currentBuilding.resources[0];
                            player.stone -= player.currentBuilding.resources[1];
                        }
                        else
                        {
                            Console.WriteLine("Not enough resources!");
                        }
                    buildingCount++;
                    }
                    else
                    {
                        Quests.CompleteQuest(map, player, Mayor, running);
                        buildingCount=0;
                    }  
                }
                else
                {
                    Console.WriteLine("Error! Incorrect input!");
                }
                if (running && stepCount<stepAmount)
                {
                    if(mayorStart)
                    {
                        Console.WriteLine($"Progress: {stepCount*100/stepAmount}%");
                        Console.WriteLine($"Current quest: {Quests.Prompts[$"Quest{stepCount+1}"]}");
                    }

                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 
/// hi :D