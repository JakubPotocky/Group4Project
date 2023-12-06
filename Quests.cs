namespace WorldOfZuul
{
    public class Quests //class for quests and associated promts
    {
        private static readonly Dictionary<string, string> _Prompts = new() //stores quests promts
        {   
            ["Quest1"]= "Build 5 houses",
            ["Quest2"]= "Build a market",
            ["Quest3"]= "Build 5 houses",
            ["Quest4"]= "Build a workspace",
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
            ["Quest17"]= "Build a workspace",
            ["Quest18"]= "Build 10 houses",
            ["Quest19"]= "Build a shopping mall",
            ["Quest20"]= "Build a stadium"
        }; //we have decided not to add, for example, the bank. 
        //in our opinion, it would be difficult and requires payment development 
        private static readonly Dictionary<int, Blueprint> blueprintForQuest = new()
        { // Dictionary linking quest numbers to corresponding building objects
            [1]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [2]= new IndustrialBlueprint("Market", 'm', 1, new List<int>{5,5}, 1, 2),
            [3]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [4]= new IndustrialBlueprint("Workspace", 'w',  1, new List<int>{5,5}, 2, -5),
            [5]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [6]= new IndustrialBlueprint("Market", 'm',  1, new List<int>{5,5}, 1, 2),
            [7]= new IndustrialBlueprint("City hall", 't',  1, new List<int>{5,5}, 2, 0),
            [8]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [9]= new IndustrialBlueprint("Hospital", 'h',  1, new List<int>{5,5}, 2, 5),
            [10]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [11]= new IndustrialBlueprint("School", 'e',  1, new List<int>{5,5},  2, 3),
            [12]= new IndustrialBlueprint("Market", 'm',  1, new List<int>{5,5}, 1, 2),
            [13]= new IndustrialBlueprint("Police department", 'p',  1, new List<int>{5,5}, 2, 4),
            [14]= new IndustrialBlueprint("Park", 'c',  1, new List<int>{5,5}, 1, 5),
            [15]= new HouseBlueprint("House", 'l', 5, new List<int>{5,5}, 5, 10),
            [16]= new IndustrialBlueprint("Fire Department", 'f',  1, new List<int>{5,5}, 2, 4),
            [17]= new IndustrialBlueprint("Workspace", 'w',  1, new List<int>{5,5}, 2, -5),
            [18]= new HouseBlueprint("House", 'l', 10, new List<int>{5,5}, 5, 10),
            [19]= new IndustrialBlueprint("Shopping mall", 'b',  1, new List<int>{5,5}, 2, 4),
            [20]= new IndustrialBlueprint("Stadium", 's',  1, new List<int>{5,5}, 2, 3)
        };

        public static Dictionary<string, string> Prompts
        {
            get
            {
                return _Prompts;
            }
        }

        public static void StartQuest(int questNum, User player)
        {
            player.currentBlueprint = blueprintForQuest[questNum];
        }
        public static void CompleteQuest(Map map, User player, NPC Mayor, bool running)
        {
            Program.stepCount++;
            // Check if there are more quests to continue or if the game is over
            if(Program.stepCount < Program.stepAmount)
            { //Promt for the next quest
                Console.WriteLine(Mayor.GetPrompt($"Quest{Program.stepCount+1}"));
                StartQuest(Program.stepCount+1, player);
            }
            else
            { //Show game results and farewell message
                Console.WriteLine(Mayor.GetPrompt("Goodbye"));
                Console.WriteLine("Here's how your city looked at the end: ");
                map.Print(null);
                Functions.ImpactBuildings(map);
                int houseScore = Functions.CalculateHouseScore();
                Console.WriteLine($"Final score: {houseScore}");//for now
                Program.running=false;
            }
        }
    }
}
