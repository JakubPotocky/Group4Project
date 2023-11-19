namespace WorldOfZuul
{
    public class Functions
    {
        public static void PrintMapLegend()
        {
            Console.WriteLine("Legend:\n⚐-Player\n♧-Trees\n♦-Plain\n∆-Mines\n≋-Water\nM-Mayor");
        }

        public static void PrintUserOptions(Square playerSquare)
        { ///create inventory system +add checking inventory system here
            bool isOnMayor = false;
            if(playerSquare.value == 'M')
            {
                Console.WriteLine(Program.Mayor.GetPrompt("Introduction"));
                playerSquare.value = '♦';
                Program.mayorStart = true;
                isOnMayor = true;
            }
            Console.WriteLine("\nWhich direction do you want to go?\nD-Right\nW-Up\nA-Left\nS-Down\nQ-Quit\n\nExtra Options:\nC-Complete a step\nL-Map Legend");
            if (!isOnMayor)
            {
                if (playerSquare.value == '♦') //And has the option to place a building (check inventory) and has the resources necessary
                    Console.WriteLine("B - Place a building"); //Later get building name from inventory and put it here (Place a {inventory.building.name})
                else if (playerSquare.value == '♧')
                {
                    Console.WriteLine("X - Cut down the tree\nP - Permanently cut down the tree");
                }
                else if (playerSquare.value == '∆')
                {
                    Console.WriteLine("X - Mine stone");//do we display mine man ?
                }
                //else if it's a building give the option to use a shovel ??????
            }
        }
    }
}