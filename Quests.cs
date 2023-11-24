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
            [1]= new House("House", new List<int>{5,5}, 5, 10),
            [2]= new Industrial("Market", 'm', new List<int>{5,5}, 5, 5, null),
            [3]= new House("House", new List<int>{5,5}, 5, 10),
            [4]= new Industrial("Workspace", 'w', new List<int>{5,5}, 5, 5, null),
            [5]= new House("House", new List<int>{5,5}, 5, 10),
            [6]= new Industrial("Market", 'm', new List<int>{5,5}, 5, 5, null),
            [7]= new Industrial("City hall", 't', new List<int>{5,5}, 5, 5, null),
            [8]= new House("House",new List<int>{5,5}, 5, 10),
            [9]= new Industrial("Hospital", 'h', new List<int>{5,5}, 5, 5, null),
            [10]= new House("House", new List<int>{5,5}, 5, 10),
            [11]= new Industrial("School", 'e', new List<int>{5,5}, 5, 5, null),
            [12]= new Industrial("Market", 'm', new List<int>{5,5}, 5, 5, null),
            [13]= new Industrial("Police department", 'p', new List<int>{5,5}, 5, 5, null),
            [14]= new Industrial("Park", 'c', new List<int>{5,5}, 5, 5, null),
            [15]= new House("House", new List<int>{5,5}, 5, 10),
            [16]= new Industrial("Fire Department", 'f', new List<int>{5,5}, 5, 5, null),
            [17]= new Industrial("Workspace", 'w', new List<int>{5,5}, 5, 5, null),
            [18]= new House("House", new List<int>{5,5}, 5, 10),
            [19]= new Industrial("Shopping mall", 'b', new List<int>{5,5}, 5, 5, null),
            [20]= new Industrial("Stadium", 's', new List<int>{5,5}, 5, 5, null)
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
                Console.WriteLine("Congrats on completing the game! Thank you for playing EcoCity: Building a Sustainable Future!\nScore:100/100\nHere's how your city looked at the end:\n");
                map.Print(null);
                Program.running=false;
            }
        }
    }
}