namespace WorldOfZuul
{
    public class Quests //class for quests and associated promts
    {
        private static readonly Dictionary<string, string> _Prompts = new() //stores quests promts
        {   
            ["Quest1"]= "Build 5 houses",
            ["Quest2"]= "Build a market",
            ["Quest3"]= "Build 5 houses",
            ["Quest4"]= "Build a factory",
            ["Quest5"]= "Build 5 houses",
            ["Quest6"]= "Build a market",
            ["Quest7"]= "Build a city hall",
            ["Quest8"]= "Build 5 houses",
            ["Quest9"]= "Build a hospital",
            ["Quest10"]= "Build 5 houses",
            ["Quest11"]= "Build a school",
            ["Quest12"]= "Build a market",
            ["Quest13"]= "Build a police department",
            ["Quest14"]= "Build a park",
            ["Quest15"]= "Build 5 houses",
            ["Quest16"]= "Build a fire department",
            ["Quest17"]= "Build a factory",
            ["Quest18"]= "Build 10 houses",
            ["Quest19"]= "Build a shopping mall",
            ["Quest20"]= "Build a stadium"
        }; //we have decided not to add, for example, the bank. 
        //in our opinion, it would be difficult and requires payment development 
        private static readonly Dictionary<int, Blueprint> blueprintForQuest = new()
        { // Dictionary linking quest numbers to corresponding building objects
            [1]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [2]= new IndustrialBlueprint("Market", 'm', 1, new List<int>{20,10}, 1, 4, null),
            [3]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [4]= new IndustrialBlueprint("Factory", 'w',  1, new List<int>{0,25}, 2, -10, "Metal"),
            [5]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [6]= new IndustrialBlueprint("Market", 'm',  1, new List<int>{20,10}, 1, 4, null),
            [7]= new IndustrialBlueprint("City hall", 't',  1, new List<int>{0,25}, 2, 0, "Metal"),
            [8]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [9]= new IndustrialBlueprint("Hospital", 'h',  1, new List<int>{20,20}, 2, 10, "Metal"),
            [10]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [11]= new IndustrialBlueprint("School", 'e',  1, new List<int>{25,10},  2, 6, null),
            [12]= new IndustrialBlueprint("Market", 'm',  1, new List<int>{20,10}, 1, 4, "Metal"),
            [13]= new IndustrialBlueprint("Police department", 'p',  1, new List<int>{20,10}, 2, 8, "Metal"),
            [14]= new IndustrialBlueprint("Park", 'c',  1, new List<int>{30,0}, 1, 10, null),
            [15]= new HouseBlueprint("House", 'l', 5, new List<int>{10,5}, 5, 10),
            [16]= new IndustrialBlueprint("Fire Department", 'f',  1, new List<int>{10,20}, 2, 8, "Metal"),
            [17]= new IndustrialBlueprint("Factory", 'w',  1, new List<int>{0,25}, 2, -10, "Metal"),
            [18]= new HouseBlueprint("House", 'l', 10, new List<int>{5,5}, 5, 10),
            [19]= new IndustrialBlueprint("Shopping mall", 'b',  1, new List<int>{40,20}, 2, 8, null),
            [20]= new IndustrialBlueprint("Stadium", 's',  1, new List<int>{50,25}, 2, 6, "Metal")
        };

        public static Dictionary<string, string> Prompts
        {
            get
            {
                return _Prompts;
            }
        }

        public static void StartQuest(int questNum, User player, NPC Mayor)
        {
            Program.regrowingTrees.Clear();
            Program.regeneratingMines.Clear();
            player.extraResource = null;
            player.currentBlueprint = blueprintForQuest[questNum];
        }
        public static void CompleteQuest(Map map, User player, NPC Mayor, bool running)
        {
            Program.stepCount++;
            // Check if there are more quests to continue or if the game is over
            if(Program.stepCount < Program.stepAmount)
            { //Promt for the next quest
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Last City Mayor:"); 
                Console.ResetColor();
                Console.WriteLine(Mayor.GetPrompt($"Quest{Program.stepCount+1}"));
                StartQuest(Program.stepCount+1, player, Mayor);
            }
            else
            { //Show game results and farewell message
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Last City Mayor:"); 
                Console.ResetColor();
                Console.WriteLine(Mayor.GetPrompt("Goodbye"));
                Console.WriteLine("Here's how your city looked at the end: ");
                map.Print(null);
                Functions.ImpactBuildings(map);
                Functions.TreeImpactBuildings(map);
                float finalScore = 0;
                float houseScore = Functions.CalculateHouseScore();
                finalScore += houseScore;
                float treeMultiplier = ((float)map.tree_coords.Count/10f)-0.3f; // 0.7 -> 1.2
                Math.Round(finalScore, 2);
                finalScore *= Functions.CalculateHallImpact();
                Math.Round(finalScore, 2);
                finalScore *= treeMultiplier;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Final score: ");//for now
                Console.ResetColor();
                Console.Write($"{(int)finalScore}\n\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Sustainability level: ");
                Console.ResetColor();
                if ((int)finalScore < 50)
                 Console.Write("Very low\n");
                else if (50 <= (int)finalScore && (int)finalScore < 100) 
                 Console.Write("Low\n");
                else if (100 <= (int)finalScore && (int)finalScore < 200) 
                 Console.Write("Medium\n");
                else if (200 <= (int)finalScore && (int)finalScore < 240) 
                 Console.Write("High\n");
                else
                 Console.Write("Very high\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nThanks for playing! :)");
                Console.ResetColor();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                Program.running=false;
            }
        }
    }
}
