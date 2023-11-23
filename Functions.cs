namespace WorldOfZuul
{
    public class Functions
    {
        public static void PrintMapLegend()
        {//change this ?
            Console.WriteLine("Legend:\n⚐-Player\n♧-Trees\n♦-Plain\n∆-Mines\n≋-Water\nM-Mayor\n☖-Houses\n☗-Market\n☢-Factory\n♥-City hall\n♱-Hospital\nS-School\nP-Police department\nT-Park\nF-Fire department\n$-Big shop\nO-Stadium");
        }
        public static void PrintUserOptions(User player)
        { ///create inventory system +add checking inventory system here
            if(player.currentSquare.value == 'M')
            {
                Console.WriteLine("Last City Mayor:"); 
                Console.WriteLine(Program.Mayor.GetPrompt("Introduction"));
                Console.WriteLine();
                Console.WriteLine(Program.Mayor.GetPrompt("Quest1"));
                Quests.StartQuest(1, player);
                player.currentSquare.value = '♦';
                Program.mayorStart = true;
            }
            if (player.currentSquare.value == '∆')
            {
                if (!Program.minerStart && Program.mayorStart)
                {
                    Console.WriteLine("Miner:");
                    Console.WriteLine(Program.Miner.GetPrompt("Introduction"));
                    Program.minerStart = true;
                }
            }

            Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\n\nExtra Options:\nL-Map Legend\nQ-Quit");
            
            if (Program.mayorStart)
            {
                if (player.currentSquare.value == '♦' && player.currentBuilding != null) //And has the option to place a building (check inventory) and has the resources necessary
                {   
                    Console.Write($"B-Place a {player.currentBuilding.name} (Resources needed: ");
                    if (player.currentBuilding.resources[0] > 0)
                    {
                        Console.Write($"{player.currentBuilding.resources[0]} Wood");
                        if (player.currentBuilding.resources[1] > 0)
                        {
                            Console.Write(", ");
                        }
                    }
                    if (player.currentBuilding.resources[1] > 0)
                    {
                        Console.Write($"{player.currentBuilding.resources[1]} Stone");

                    }
                    Console.Write(")");
                }
                //else if it's a building give the option to use a shovel ??????
            }

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
    }
}