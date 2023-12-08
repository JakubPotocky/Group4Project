namespace WorldOfZuul
{
    public class MinerPrompts
    {
        private static readonly Dictionary<string, string> _Prompts = new()
        {
            ["Introduction"]= "Hi, my name is John and I am the city's miner. My friend, the last mayor, told me about you and your exceptional goal, so I'll be helping you with 5 hints for buildings positions as I also happen know some things about sustainability, call me here at the mountains when you need one! PSST! And as a good start of our friendship I'll give you a bonus tip: the trees you see in the center will have a great influence on your final score as they'll keep your city center less polluted and more green, so cut them down only if you must.",
            ["Exceed"]= "I've spilled all I know, and I'm drawing a blank. But don't give up! Chat with others in town or explore more!",
            ["Quest1"]= "Hey there! For cozy homes with a nice view, consider placing houses near the outskirts. Leave some space between them for a neighborly feel.",
            ["Quest2"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest3"]= "Hey there! If you want a higher sustainability score try placing houses near the market:) You see, when homes are close to the market, it encourages a more localized lifestyle. People can easily walk or bike to the market, reducing the need for cars and cutting down on harmful emissions.",
            ["Quest4"]= "So, friend? I suggest you build a factory on the outskirts of the city, so it doesn't ruin the air quality and contribute to the pollution problem we've been grappling with. Our town has been a haven of clean air and clear skies, and we'd like to keep it that way.",
            ["Quest5"]= "Hey there! If you want a higher sustainability score try placing houses far from the factory, for less pollution",
            ["Quest6"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest7"]= "City hall, the beating heart of our town! Keep in mind positioning it will influence the score greatly, so you better center it, surrounded by the markets, school, and the other main buildings for a well-organized city!",
            ["Quest8"]= "Hey there! If you want a higher sustainability score try placing houses near the market:) You see, when homes are close to the market, it encourages a more localized lifestyle. People can easily walk or bike to the market, reducing the need for cars and cutting down on harmful emissions.",
            ["Quest9"]= "For a healthy town, put the hospital close to the center. Quick access in emergencies, you know? It's all about keeping our folks safe and sound.",
            ["Quest10"]= "Hey there! If you want a higher sustainability score try placing houses near the hospital. It's not just about convenience; it's about creating a healthier, more sustainable community. When homes are close to the hospital, it means quicker access to medical care and emergency services. That alone can make a significant difference in people's lives.",
            ["Quest11"]= "Spread those schools out evenly in residential areas. Kids shouldn't travel too far. And keep them away from noisy factories for better learning conditions.",
            ["Quest12"]= "The market's heart should be close to the city's pulse. Find a spot near the center or close to where folks work to keep things bustling.",
            ["Quest13"]= "Keep our streets safe! Put the police department where it can cover both homes and businesses. Safety first, always!",
            ["Quest14"]= "A breath of fresh air, literally! Stick parks in residential spots. Near water or trees, if you can, for a peaceful and green touch.",
            ["Quest15"]= "Hey there! If you want a higher sustainability score try placing houses near the police department, by doing that you're cultivating a lifestyle that prioritizes safety and resilience.)",
            ["Quest16"]= "Fire safety matters! Place the fire department centrally, so they can reach any part of the city quickly. Close to homes and industries for quick response.",
            ["Quest17"]= "So, friend? I suggest you build a factory on the outskirts of the city, so it doesn't ruin the air quality and contribute to the pollution problem we've been grappling with. Our town has been a haven of clean air and clear skies, and we'd like to keep it that way.",
            ["Quest18"]= "Hey there! If you want a higher sustainability score try placing houses near the fire department. Consider the wider picture—a safe community requires fewer resources for firefighting and disaster response. This allows the town to use resources more efficiently, contributing to a sustainable approach that benefits both individuals and the community as a whole.",
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