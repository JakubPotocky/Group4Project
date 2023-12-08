namespace WorldOfZuul
{
    public class MinerPrompts
    {
        private static readonly Dictionary<string, string> _Prompts = new()
        {
            ["Introduction"]= "Hi, my name is John and I am the city's miner. My friend, the last mayor, told me about you and your exceptional goal, so I'll be helping you with 5 hints for your buildings positions as I also happen know some things about sustainability, call me here at the mountains when you need one!",
            ["Exceed"]= "I've spilled all I know, and I'm drawing a blank. But don't give up! Chat with others in town or explore more!",
            ["Quest1"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest2"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest3"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest4"]= "So, friend? I suggest you build a factory on the outskirts of the city, but not too far, and not near the water, in order to avoid polluting the waters.",
            ["Quest5"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest6"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest7"]= "City hall, the beating heart of our town! Center it all, surrounded by the market, workplaces, and schools for a well-organized city!",
            ["Quest8"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest9"]= "For a healthy town, put the hospital close to the center. Quick access in emergencies, you know? It's all about keeping our folks safe and sound.",
            ["Quest10"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest11"]= "Spread those schools out evenly in residential areas. Kids shouldn't travel too far. And keep them away from noisy factories for better learning conditions.",
            ["Quest12"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest13"]= "Keep our streets safe! Put the police department where it can cover both homes and businesses. Safety first, always!",
            ["Quest14"]= "A breath of fresh air, literally! Stick parks in residential spots. Near water or trees, if you can, for a peaceful and green touch.",
            ["Quest15"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest16"]= "Fire safety matters! Place the fire department centrally, so they can reach any part of the city quickly. Close to homes and industries for quick response.",
            ["Quest17"]= "So, friend? I suggest you build a factory on the outskirts of the city, but not too far, and not near the water, in order to avoid polluting the waters.",
            ["Quest18"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest19"]= "Shopping time! Big shops work well near the market or commercial areas. Make 'em easy to reach for everyone in the city.",
            ["Quest20"]= "For some sports and leisure, build the stadium on the outskirts. Keep the noise away from homes, maybe near a park for a more relaxed vibe."
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