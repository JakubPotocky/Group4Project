namespace WorldOfZuul
{
    public class Quests
    {
        private static readonly Dictionary<string, string> _Prompts = new()
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
        };
        private static readonly Dictionary<int, Building> buildingForQuest = new()
        {
            [1]= new House("House", 5, new List<int>{5,5}, 5, 10),
            [2]= new Industrial("Market", 'm', 1, new List<int>{5,5}, 1, 2, null),
            [3]= new House("House", 5, new List<int>{5,5}, 5, 10),
            [4]= new Industrial("Workspace", 'w', 1, new List<int>{5,5}, 2, -10, null),
            [5]= new House("House", 5,new List<int>{5,5}, 5, 10),
            [6]= new Industrial("Market", 'm', 1, new List<int>{5,5}, 1, 2, null),
            [7]= new Industrial("City hall", 't', 1, new List<int>{5,5}, 2, 0, null),
            [8]= new House("House", 5, new List<int>{5,5}, 5, 10),
            [9]= new Industrial("Hospital", 'h', 1, new List<int>{5,5}, 2, 5, null),
            [10]= new House("House", 5, new List<int>{5,5}, 5, 10),
            [11]= new Industrial("School", 'e', 1, new List<int>{5,5}, 2, 3, null),
            [12]= new Industrial("Market", 'm', 1, new List<int>{5,5}, 1, 2, null),
            [13]= new Industrial("Police department", 'p', 1, new List<int>{5,5}, 2, 4, null),
            [14]= new Industrial("Park", 'c', 1, new List<int>{5,5}, 1, 5, null),
            [15]= new House("House", 5, new List<int>{5,5}, 5, 10),
            [16]= new Industrial("Fire Department", 'f', 1, new List<int>{5,5}, 2, 4, null),
            [17]= new Industrial("Workspace", 'w', 1, new List<int>{5,5}, 2, -10, null),
            [18]= new House("House", 10, new List<int>{5,5}, 5, 10),
            [19]= new Industrial("Shopping mall", 'b', 1, new List<int>{5,5}, 2, 4, null),
            [20]= new Industrial("Stadium", 's', 1, new List<int>{5,5}, 2, 3, null)
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
            player.currentBuilding = buildingForQuest[questNum];
        }
        public static void CompleteQuest(Map map, User player, NPC Mayor, bool running)
        {
            Program.stepCount++;
            if(Program.stepCount < Program.stepAmount)
            {
                Console.WriteLine(Mayor.GetPrompt($"Quest{Program.stepCount+1}"));
                StartQuest(Program.stepCount+1, player);
            }
            else
            {
                Console.WriteLine(Mayor.GetPrompt("Goodbye"));
                Console.WriteLine("Here's how your city looked at the end: ");
                map.Print(null);
                Functions.ImpactBuildings(map);
                float houseScore = Functions.CalculateHouseScore();
                Console.WriteLine($"Final score: {houseScore}");//for now
                Program.running=false;
            }
        }
    }
}
