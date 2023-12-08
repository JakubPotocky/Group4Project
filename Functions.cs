namespace WorldOfZuul
{
    public class Functions
    {
        // Print the legend of symbols representing different elements on the map
        public static void PrintMapLegend()
        {//change this ?
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Legend:");
            Console.ResetColor();
            Console.WriteLine("🫅  -Player\n🌳 -Trees\n🟩 -Plain\n🏔️  -Mountains\n🌊 -Water\n👔 -Mayor\n🏠 -Houses\n🏪 -Market\n🏭 -Factory\n🏛️  -City hall\n🏥 -Hospital\n🏫 -School\n🏬 -Police department\n🏞️  -Park\n🚒 -Fire department\n💸 -Big shop\n🏟️  -Stadium\n");
        }

        // Print user options based on the player's current location and game progress
        public static void PrintUserOptions(User player)
        { 
            //create inventory system +add checking inventory system here
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWhich direction do you want to go?");
            Console.ResetColor();
            Console.WriteLine("↑/W-Up\n←/A-Left\n↓/S-Down\n→/D-Right\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Extra Options:");
            Console.ResetColor();
            Console.WriteLine("L-Map Legend\nQ-Quit");
            

            if(Program.dwarf.buildingInShovel == null)
            {
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
                        if(player.currentBlueprint is IndustrialBlueprint)
                        {
                            IndustrialBlueprint industrialBlueprint = player.currentBlueprint as IndustrialBlueprint;
                            if(industrialBlueprint.extraResource != null)
                            {
                                Console.Write($", 1 {industrialBlueprint.extraResource}");
                            }
                        }
                        Console.WriteLine(")");
                    }
                }
                // Check if the player is at a square with trees
                if (player.currentSquare.value == '♧' && !ContainsEqualCoordinates(Program.regrowingTrees, player.currentCoords))
                {
                    Console.WriteLine("X-Harvest some wood");
                    if (player.currentCoords[1] != 9)
                    {
                        Console.WriteLine("P-Permanently cut down the trees");
                    }
                }
                else if (player.currentSquare.value == '∆' && !ContainsEqualCoordinates(Program.regeneratingMines, player.currentCoords))
                {
                    Console.WriteLine("X-Mine stone");//do we display mine man ?
                }
                if (player.currentSquare.obj != null && player.shovelsLeft != 0 && Program.dwarfStart)
                {
                    Console.WriteLine("G-Call for Ugly Dwarf to move the building");
                }
                if(player.currentSquare.value == '∆' && player.hintsLeft != 0 && Program.minerStart)
                {
                    Console.WriteLine("H-Ask mineman for hint");
                    Console.WriteLine("M-Repeat mayor's last line");
                }
                if(player.currentSquare.value == 'C' && player.currentBlueprint is IndustrialBlueprint && player.extraResource == null)
                {
                    IndustrialBlueprint industrialBlueprint = player.currentBlueprint as IndustrialBlueprint;
                    if(industrialBlueprint.extraResource != null)
                    {
                        Console.WriteLine("T-Trade with Captain");
                    }
                }
            }
            else
            {
                if (player.currentSquare.obj != null)
                {
                    Console.WriteLine("G-Place down the building");
                }
            }

        }
        public static bool ContainsEqualCoordinates(List<List<int>> containList, List<int> searchCoords)
        {
            foreach(List<int> coordinates in containList)
            {
                if(EqualCoordinates(coordinates, searchCoords))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool EqualCoordinates(List<int> coords1, List<int> coords2)
        {
            if(coords1[0] == coords2[0] && coords1[1] == coords2[1])
            {
                return true;
            }
            return false;
        }
        public static void ImpactBuildings(Map map)
        {
            foreach (Industrial industrial in Industrial.all)
            {
                industrial.ImpactHouses(map);
            }
        }
        public static void TreeImpactBuildings(Map map)
        {
            foreach(List<int> tree_coord in map.tree_coords)
            {
                int tree_x = tree_coord[0];
                int tree_y = tree_coord[1];
                for (int i=-1;i<=1; i++)
                {
                    for(int j=-1;j<=1;j++)
                    {
                        if((i==0 || j==0) && Enumerable.Range(0, 10).Contains(tree_x + i) && Enumerable.Range(0, 10).Contains(tree_y + j))
                        {
                            Square square = map.this_map[tree_y+j][tree_x+i];
                            if(square.obj is House)
                            {
                                House house = square.obj as House;
                                house.survivabilityIndex += 4;
                            }
                        }
                    }
                }
            }
        }
        public static float CalculateHouseScore()
        {
            float score = 0f;
            foreach (House house in House.all.Values)
            {
                if(house.survivabilityIndex <= 1)
                {
                    score += 0;
                }
                else
                {
                    score += (1f - 1f/house.survivabilityIndex) * house.inhabitants;
                }
            }
            return score;
        }
        public static float CalculateHallImpact()
        {
            foreach(Industrial industrial in Industrial.all)
            {
                if(industrial.name == "City hall")
                {
                    List<int> coords = industrial.coordinates;
                    if(Enumerable.Range(4, 2).Contains(coords[0]) && Enumerable.Range(4, 2).Contains(coords[1]))
                    {
                        return 1.1f;
                    }
                    else if(Enumerable.Range(3, 4).Contains(coords[0]) && Enumerable.Range(3, 4).Contains(coords[1]))
                    {
                        return 1f;
                    }
                    else if(Enumerable.Range(2, 6).Contains(coords[0]) && Enumerable.Range(2, 6).Contains(coords[1]))
                    {
                        return 0.9f;
                    }
                    else if(Enumerable.Range(1, 8).Contains(coords[0]) && Enumerable.Range(1, 8).Contains(coords[1]))
                    {
                        return 0.8f;
                    }
                    else
                    {
                        return 0.7f;
                    }
                }
            }
            return 1f;
        }
    }
}