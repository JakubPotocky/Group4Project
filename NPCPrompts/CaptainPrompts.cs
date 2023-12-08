namespace WorldOfZuul
{
    public class CaptainPrompts
    {
        private static readonly Dictionary<string, string> _Prompts = new()
        {
            ["Introduction"]= " In a lively city, where nature meets progress, I'm your trusty Captain. My special knack? Turning wood and stone into shiny metal. The big shots in the city want me to build a snazzy center with a super high-tech factory. To pull it off, I've got to find these special rocks called Core Stones hidden around town.\nBut it's not all smooth sailing. There are other groups in the city who want to put the brakes on my plans. They're not going for the tough stuff, though. They're using tricks like playing politics and doing sneaky business moves.\nThat's where we come in, me and you! We've got to team up to make sure the city grows with cool tech but keeps the natural beauty intact. Our journey's just beginning, and the city's future is in our hands. Let's get to it!!",
            ["Quest4"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest7"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest9"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest12"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest13"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest16"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest17"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Quest20"]= "I can sell you 1 metal for 10 wood and 10 stone!",
            ["Other"]= "I have nothing to offer you for now!"
        };
        public static Dictionary<string, string> Prompts
        {
            get
            {
                return _Prompts;
            }
        }
    }
}