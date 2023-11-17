using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace WorldOfZuul
{
    public class Program
    {
        public static void Main()
        {
            bool running = true;

            int stepAmount = 20;

            int stepCount = 0;

            //string[] NPCprompts = File.ReadAllLines("NPCprompts/");

            string[] Quests = File.ReadAllLines("Quests.txt");
            
            Map map = new();
            int xSize = 10; //10
            int ySize = 10;

            map.Initialize(ySize, xSize);

            User player = new(map);

            NPC Mayor = new("Mayor", MayorPrompts.Prompts);
            // Welcome to the game
            Console.WriteLine(Mayor.GetPrompt("Introduction"));
            map.Print(player.currentCoords);

            while (running)
            {
                Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\nQ-Quit\n\nExtra Options:\nC-Complete a step\n"); //instructions
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
                
                if (userChoice == "q")
                {
                    running = false;
                }
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
                else if(userChoice == "c")
                {
                    if(stepCount < stepAmount)
                    {
                        stepCount++;
                        Console.WriteLine(Mayor.GetPrompt($"Quest{stepCount}"));
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Incorrect input!");
                }
                if (running)
                {
                    Console.WriteLine($"User is standing on {player.currentSquare.value}");
                    Console.WriteLine($"Steps ?XD *DONT FORGET TO CHAGE THIS*: {stepCount}/{stepAmount}");
                    Console.WriteLine($"Current quest: {Quests[stepCount]}");
                    map.Print(player.currentCoords);
                }
            }
        }
    }
}
/// -> give more options to player that is based on current square ...
/// VALIDATE INPUT ✔️ 
/// hi :D