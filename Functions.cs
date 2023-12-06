namespace WorldOfZuul
{
    public class Functions
    {
        // Print the legend of symbols representing different elements on the map
        public static void PrintMapLegend()
        {//change this ?
            Console.WriteLine("Legend:\n🫅  -Player\n🌳 -Trees\n🟩 -Plain\n🏔️  -Mines\n🌊 -Water\n👔 -Mayor\n🏠 -Houses\n🏪 -Market\n🏭 -Factory\n🏛️  -City hall\n🏥 -Hospital\n🏫 -School\n🏬 -Police department\n🏞️  -Park\n🚒 -Fire department\n💸 -Big shop\n🏟️  -Stadium\n");
        }

        // Print user options based on the player's current location and game progress
        public static void PrintUserOptions(User player)
        { 
            //create inventory system +add checking inventory system here
            Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\n\nExtra Options:\nL-Map Legend\nQ-Quit");
            
            if (Program.mayorStart)
            {
                if (player.currentSquare.value == '♦' && player.currentBlueprint != null) // Check if the player can place a building and has the required resources
                //And has the option to place a building (check inventory) and has the resources necessary
                {   
                    Console.Write($"B-Place a {player.currentBlueprint.name} (Resources needed: ");
                    if (player.currentBlueprint.resources[0] > 0)
                    {
                        Console.Write($"{player.currentBlueprint.resources[0]} Wood");
                        if (player.currentBlueprint.resources[1] > 0)
                        {
                            Console.Write(", ");
                        }
                    }
                    if (player.currentBlueprint.resources[1] > 0)
                    {
                        Console.Write($"{player.currentBlueprint.resources[1]} Stone");

                    }
                    Console.Write(")");
                }
                //else if it's a building give the option to use a shovel ??????
            }

            // Check if the player is at a square with trees
            if (player.currentSquare.value == '♧')
            {
                Console.WriteLine("X-Cut down the trees\nP-Permanently cut down the trees");
            }
            else if (player.currentSquare.value == '∆')
            {
                Console.WriteLine("X-Mine stone");//do we display mine man ?
            }
            // else if (player.currentSquare.value == '♦')
            // {
            //     Console.WriteLine("T-Plant trees");
            // }
            if(player.currentSquare.value == '∆' && player.hintsLeft != 0 && Program.minerStart)
            {
                Console.WriteLine("H - Ask mineman for hint");
            }
        }
        public static void ImpactBuildings(Map map)
        {
            foreach (Industrial industrial in Industrial.all)
            {
                industrial.ImpactHouses(map);
            }
        }
        public static int CalculateHouseScore()
        {
            float score = 0f;
            foreach (House house in House.all.Values)
            {
                score += (1f - 1f/house.survivabilityIndex) * house.inhabitants;
            }
            return (int)score;
        }
    }
}