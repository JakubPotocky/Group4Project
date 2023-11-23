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
        public static void Main()
        {
            bool running = true;

            int stepAmount = 20;

            int stepCount = 0;

            int wood = 0;

            int stone = 0;

            //string[] NPCprompts = File.ReadAllLines("NPCprompts/");

            string[] Quests = File.ReadAllLines("Quests.txt");
            
            Map map = new();
            int xSize = 10; //10
            int ySize = 10;
            int i=0;
            
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
                    Functions.PrintUserOptions(player.currentSquare);
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
                else if(mayorStart && userChoice == "c") //Quests/Steps
                {
                    stepCount++;
                    if(stepCount < stepAmount)
                    {
                        Console.WriteLine(Mayor.GetPrompt($"Quest{stepCount+1}"));
                    }
                    else
                    {
                        Console.WriteLine("Congrats on completing the game! Thank you for playing EcoCity: Building a Sustainable Future!\nScore:100/100\nHere's how your city looked at the end:\n");
                        map.Print(null);
                        running=false;
                    }
                }
                else if(userChoice == "l") //Legend
                {
                    Functions.PrintMapLegend();
                }
                else if(userChoice == "x" && player.currentSquare.value == '♧') //cutting trees
                {
                    //player.currentSquare.value = '♦';
                    wood+=5;
                }
                else if(userChoice == "p" && player.currentSquare.value == '♧') //cutting trees permanently 
                {
                    player.currentSquare.value = '♦';
                    wood+=10;
                }
                else if(userChoice == "x" && player.currentSquare.value == '∆') //mining stone
                {
                    //player.currentSquare.value = '♦';
                    stone+=10;
                }
                else if(minerStart && userChoice == "h" && i<3) //Hints
                {
                    i++;
                    Console.WriteLine(Miner.GetPrompt($"Quest{stepCount+1}"));
                }
                else if(minerStart && userChoice == "h" && i>=3)
                    Console.WriteLine(Miner.GetPrompt($"Exceed"));
                else
                {
                    Console.WriteLine("Error! Incorrect input!");
                }
                if (running && stepCount<stepAmount)
                {
                    if(mayorStart)
                    {                                                                /// Write your name for what you vote for using in the code, deadline is end of november XD
                        Console.WriteLine($"Progress: {stepCount*100/stepAmount}%"); /// Quests: jakubP, 
                        Console.WriteLine($"Current quest: {Quests[stepCount]}");    /// Steps:
                    }

                    Console.WriteLine($"Wood: {wood}");
                    Console.WriteLine($"Stone: {stone}");

                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 
/// hi :D