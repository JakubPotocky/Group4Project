namespace WorldOfZuul
{
    public class Functions
    {
        public static void PrintMapLegend()
        {
            Console.WriteLine("Legend:\n⚐-Player\n♧-Trees\n♦-Plain\n∆-Mines\n≋-Water\nM-Mayor\n☖-Houses\n☗-Market\n☢-Factory\n♥-City hall\n♱-Hospital\nS-School\nP-Police department\nT-Park\nF-Fire department\n$-Big shop\nO-Stadium");
        }
        public static bool o = false;
        public static bool m = false;
        public static void PrintUserOptions(Square playerSquare)
        { ///create inventory system +add checking inventory system here
            bool isOnMayor = false;
            if(playerSquare.value == 'M')
            {
                Console.WriteLine("Last City Mayor:"); 
                Console.WriteLine(Program.Mayor.GetPrompt("Introduction"));
                playerSquare.value = '♦';
                Program.mayorStart = true;
                isOnMayor = true;
                o=true;
            }

            bool isOnMiner = false;
            if(playerSquare.value == 'J')
            {
                Console.WriteLine("Miner:"); 
                Console.WriteLine(Program.Miner.GetPrompt("Introduction"));
                playerSquare.value = '∆';
                Program.minerStart = true;
                isOnMiner = true;
                m=true;
            }
            Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\n\nExtra Options:\nL-Map Legend\nQ-Quit");

            if (o==true)
             Console.WriteLine("C-Complete a step\n"); 
            else
             Console.WriteLine(); 

            if (!isOnMayor && o==true)
            {
                if (playerSquare.value == '♦') //And has the option to place a building (check inventory) and has the resources necessary
                {   
                    Console.WriteLine("B-Place a building"); //Later get building name from inventory and put it here (Place a {inventory.building.name})
                    if ( !isOnMiner && m==true)
                    Console.WriteLine("H-Hint");
                }
                //else if it's a building give the option to use a shovel ??????
            }

            if (playerSquare.value == '♧')
            {
                Console.WriteLine("X-Cut down the trees\nP-Permanently cut down the trees");
            }
            else if (playerSquare.value == '∆')
            {
                Console.WriteLine("X-Mine stone");//do we display mine man ?
            }
        }
    }
}