namespace WorldOfZuul
{
    // Represents a Non-Player Character (NPC) in the game
    public class NPC
    {
        // Name of the NPC
        string name;

        // Dictionary to store prompts associated with keys
        Dictionary<string, string> prompts;
        
        // Constructor to initialize an NPC with a name and prompts
        public NPC(string name, Dictionary<string, string> prompts)
        {
            this.name = name;
            this.prompts = prompts;
        }
        
        // Get a prompt associated with a specific key
        public string GetPrompt(string key)
        {
            // Check if the key exists in the prompts dictionary
            if (prompts.TryGetValue(key, out string? value))
            {
                return value;
            }
            else
            {
                // Throw an exception if the key is not found
                throw new ArgumentException($"Key- '{key}' -not found.");
            }
        }
    }
    public class Dwarf : NPC
    {
        public Building? buildingInShovel;
        public Dwarf(string name, Dictionary<string, string> prompts) : base(name, prompts){}
    }
    public class Captain : NPC
    {
        public Captain(string name, Dictionary<string, string> prompts) : base(name, prompts){}
        public readonly Dictionary<string, List<int>> cost = new()
        {
            ["Quest4"]= new(){10, 10},
            ["Quest7"]= new(){10, 10},
            ["Quest9"]= new(){10, 10},
            ["Quest12"]= new(){10, 10},
            ["Quest13"]= new(){10, 10},
            ["Quest16"]= new(){10, 10},
            ["Quest17"]= new(){10, 10},
            ["Quest20"]= new(){10, 10}
        };
    }
}