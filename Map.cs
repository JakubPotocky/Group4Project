using WorldOfZuul;
namespace WorldOfZuul
{
    public class Map
    {
        public List<List<Square>> this_map = new(); //2d list => [[]]
 
        public void Initialize(int hor, int ver)  //2d list => [["f", ...], ...]
        {
            List<int> possible_sides = new() {0, hor-1}; //hor-1
            Random rnd = new Random();
            int mines_side_index = rnd.Next(0,2);
            int mines_row_index = rnd.Next(3,ver-3);
            List<int> mines_row_list = new() {mines_row_index-2, mines_row_index-1, mines_row_index, mines_row_index+1, mines_row_index+2};

            int central_tree_ver = rnd.Next(2,ver-3);
            int central_tree_hor = rnd.Next(2,hor-3);

            List<List<int>> possible_tree_coords = new();
            for (int possible_row=-1; possible_row<=1; possible_row++)
            {
                for (int possible_col=-1; possible_col<=1; possible_col++)
                {
                    List<int> curr_coord = new List<int>{central_tree_ver + possible_row, central_tree_hor + possible_col};
                    if (possible_row != 0 || possible_col != 0)
                    {
                        possible_tree_coords.Add(curr_coord);
                    }
                }
            }

            List<List<int>> tree_coords = new();
            List<int> central_tree_coords = new(){central_tree_ver, central_tree_hor};
            tree_coords.Add(central_tree_coords);
            
            for (int i = 0; i <= 3; i++)
            {
                int rndTreeIndex = rnd.Next(0, possible_tree_coords.Count);
                List<int> rndTree = possible_tree_coords[rndTreeIndex];
                tree_coords.Add(rndTree);
                possible_tree_coords.RemoveAt(rndTreeIndex);
            }

            for(int row=0; row<ver; row++)
            {
                this_map.Add(new List<Square>());
                for(int column=0; column<hor; column++)
                {
                    List<int> curr_coords = new() {row, column};
                    if(row == 0)
                    {
                        Square square = new('≋'); //water
                        this_map[row].Add(square);
                    }
                    else if(row == (ver - 1))
                    {
                        Square square = new('♧'); //tree
                        this_map[row].Add(square);
                    }
                    else if(mines_row_list.Contains(row) && column == possible_sides[mines_side_index])
                    {
                        Square square = new('∆'); // mines
                        this_map[row].Add(square);
                    }
                    else
                    {
                        bool found_square = false;
                        for (int z=0; z<tree_coords.Count(); z++)
                        {
                            if (tree_coords[z][0] == row && tree_coords[z][1] == column)
                            {
                                Square square = new('♧'); // random trees
                                this_map[row].Add(square);
                                found_square = true;
                                break;
                            }
                        }
                        if (!found_square)
                        {
                            Square square = new('♦'); // plain
                            this_map[row].Add(square);
                        }
                    }
                }
            }

            int mayorRow = rnd.Next(1, ver-2);

            List<Square> mayorPossibleCoords = new();

            for(int column=0; column < this_map[mayorRow].Count; column++)
            {
                Square currentSquare = this_map[mayorRow][column];
                if (currentSquare.value == '♦')
                {
                    mayorPossibleCoords.Add(currentSquare);
                }
            }
            int mayorSquareIndex = rnd.Next(0, mayorPossibleCoords.Count);
            mayorPossibleCoords[mayorSquareIndex].changeValue('M');

            // int mineRow = rnd.Next(3, ver-3);

            // List<Square> minePossibleCoords = new();

            // for(int column=0; column < this_map[mineRow].Count; column++)
            // {
            //     Square currentSquare = this_map[mineRow][column];
            //     if (currentSquare.value == '∆')
            //     {
            //         minePossibleCoords.Add(currentSquare);
            //     }
            // }
            // int mineSquareIndex = rnd.Next(0, minePossibleCoords.Count);
            // minePossibleCoords[mineSquareIndex].changeValue('J');
        }

        public void Print(List<int>? playerPosition)
        {
            Console.WriteLine();
            for(int row=0; row < this_map.Count; row++)
            {
                for(int column=0; column < this_map[row].Count; column++)
                {
                    if (playerPosition != null)
                    {
                        if (row == playerPosition[0] && column == playerPosition[1])
                        {
                            Console.Write("🫅  "); // Player
                        }
                        else if(this_map[row][column].value=='♧')
                        {
                            Console.Write("🌳  "); 
                        }
                        else if(this_map[row][column].value=='∆')
                        {
                            Console.Write("🏔️  ");
                        }
                        else if(this_map[row][column].value=='≋')
                        {
                            Console.Write("🌊  "); 
                        }
                        else if(this_map[row][column].value=='♦')
                        {
                            Console.Write("🟩  "); 
                        }
                        else if(this_map[row][column].value=='M')
                        {
                            Console.Write("👔  "); 
                        }
                        else if(this_map[row][column].value=='l')
                        {
                            Console.Write("🏠  ");
                        }
                        else if(this_map[row][column].value=='m')
                        {
                            Console.Write("🏪  ");
                        }
                        else if(this_map[row][column].value=='w')
                        {
                            Console.Write("🏭  ");
                        }
                        else if(this_map[row][column].value=='t')
                        {
                            Console.Write("🏛️  ");
                        }
                        else if(this_map[row][column].value=='h')
                        {
                            Console.Write("🏥  ");
                        }
                        else if(this_map[row][column].value=='e')
                        {
                            Console.Write("🏫  ");
                        }
                        else if(this_map[row][column].value=='p')
                        {
                            Console.Write("🏬  ");
                        }
                        else if(this_map[row][column].value=='c')
                        {
                            Console.Write("🏞️  ");
                        }
                        else if(this_map[row][column].value=='f')
                        {
                            Console.Write("🚒  ");
                        }
                        else if(this_map[row][column].value=='b')
                        {
                            Console.Write("💸  ");
                        }
                        else if(this_map[row][column].value=='s')
                        {
                            Console.Write("🏟️  ");
                        }
                        else
                            Console.Write(this_map[row][column].value);
                    }
                    else
                    {
                        if(this_map[row][column].value=='♧')
                        {
                            Console.Write("🌳  "); 
                        }
                        else if(this_map[row][column].value=='∆')
                        {
                            Console.Write("🏔️  ");
                        }
                        else if(this_map[row][column].value=='≋')
                        {
                            Console.Write("🌊  "); 
                        }
                        else if(this_map[row][column].value=='♦')
                        {
                            Console.Write("🟩  "); 
                        }
                        else if(this_map[row][column].value=='M')
                        {
                            Console.Write("👔  "); 
                        }
                        else if(this_map[row][column].value=='l')
                        {
                            Console.Write("🏠  ");
                        }
                        else if(this_map[row][column].value=='m')
                        {
                            Console.Write("🏪  ");
                        }
                        else if(this_map[row][column].value=='w')
                        {
                            Console.Write("🏭  ");
                        }
                        else if(this_map[row][column].value=='t')
                        {
                            Console.Write("🏛️  ");
                        }
                        else if(this_map[row][column].value=='h')
                        {
                            Console.Write("🏥  ");
                        }
                        else if(this_map[row][column].value=='e')
                        {
                            Console.Write("🏫  ");
                        }
                        else if(this_map[row][column].value=='p')
                        {
                            Console.Write("🏬  ");
                        }
                        else if(this_map[row][column].value=='c')
                        {
                            Console.Write("🏞️  ");
                        }
                        else if(this_map[row][column].value=='f')
                        {
                            Console.Write("🚒  ");
                        }
                        else if(this_map[row][column].value=='b')
                        {
                            Console.Write("💸  ");
                        }
                        else if(this_map[row][column].value=='s')
                        {
                            Console.Write("🏟️  ");
                        }
                        else
                            Console.Write(this_map[row][column].value);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void Change(Square squareToChange, char newValue)
        {
            squareToChange.changeValue(newValue);
        }
        public void Change(List<int> squareCoords, char newValue)
        {
            Square squareToChange = this_map[squareCoords[0]][squareCoords[1]];
            squareToChange.changeValue(newValue);
        }
    }
}