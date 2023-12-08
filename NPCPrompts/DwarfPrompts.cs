namespace WorldOfZuul
{
    public class DwarfPrompts
    {
        private static readonly Dictionary<string, string> _Prompts = new()
        {
            ["Introduction"]= "Hello, I am the Ugly Dwarf of the woods. At least the town mayor called me that. Ever since then I've been known as the Ugly dwarf of the mountains. Anywho, I am here to help you with your mistakes. I am sort of the magical dwarf, if you will, Whatever mistakes you have made - i can fix them. But do not get too comfortable making the mistakes. I can only help you only a limited amount of times. You get 5 chances to corect your fails. So now you know - whenever you need help just come to me. I am here for you. I will be sitting here.",
            ["Error1"]= "Here we meet again, huh? What's the problem this time? Spit it out, and let's see if the Ugly Dwarf can do something about it. No promises on making your life all sunshine and rainbows, though.",
            ["Error2"]= "Back already, huh? What's the problem this time? Spit it out, and let's see if the Ugly Dwarf can do something about it. No promises on making your life all sunshine and rainbows, though.",
            ["Error3"]= "Alright, you're back. What's the scoop this time? Third time's the charm, or so they say. Let's get on with it. What's the deal, and can we wrap it up quick?",
            ["Error4"]= "Here we go again. This is your fourth visit to the Ugly Dwarf. Time's running out, so spill it. What's the issue this round, and can we make this snappy?  ",
            ["Error5"]= "Last shot, huh? Fifth and final time you get to bother the Ugly Dwarf. Lay out your troubles, but don't expect any sympathy. We're almost done here, so make it quick.",
            ["Goodbye"]= "As you drag yourself out, there's a lingering sting. The Ugly Dwarf's done with your drama, and you're left to face your own mess. No more hand-holding, just the harsh truth. The door slams shut, and you're alone with the echoes of your mistakes. Guess it's time to grow up, or not. The Ugly Dwarf won't be around for your pity party anymore, this is the last goodbye. So long summoner.",
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