namespace WorldOfZuul
{
    public class NPC
    {
        string name;
        Dictionary<string, string> prompts;
        
        public NPC(string name, Dictionary<string, string> prompts)
        {
            this.name = name;
            this.prompts = prompts;
        }
        
        public string GetPrompt(string key)
        {
            if (prompts.TryGetValue(key, out string? value))
            {
                return value;
            }
            else
            {
                throw new ArgumentException($"Key- '{key}' -not found.");
            }
        }
    }
}