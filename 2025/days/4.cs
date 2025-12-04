using AoC_Day;

namespace AoC2025_Day4
{
    class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            rawInput.Insert(0, new string('.', rawInput[0].Length));
            rawInput.Add(new string('.', rawInput[0].Length));
            var grid = 
                rawInput.Select(line => "." + line + ".")
                .Select(line => line.ToCharArray()).ToArray();
            
            var result = 0;


            //print grid
            Console.WriteLine(grid.Length);
            Console.WriteLine(grid[0].Length);
            for (int x = 0; x < grid.Length; x++)
            {
                var line = grid[x];
                for (int y = 0; y < line.Length; y++)
                {
                    Console.Write(grid[x][y]);

                    if (grid[x][y] == '@')
                    {
                        //check surroundings
                        var surroundings = new List<char>
                        {
                            grid[x - 1][y - 1], grid[x - 1][y], grid[x - 1][y + 1],
                            grid[x][y - 1],                     grid[x][y + 1],
                            grid[x + 1][y - 1], grid[x + 1][y], grid[x + 1][y + 1],
                        };

                        int count = surroundings.Count(c => c == '@');
                        if (count < 4)
                        {
                            result ++;
                        }
                    }
                }
            
                Console.WriteLine();
            }
            Console.WriteLine("RESULT: " + result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            rawInput.Insert(0, new string('.', rawInput[0].Length));
            rawInput.Add(new string('.', rawInput[0].Length));
            var grid = 
                rawInput.Select(line => "." + line + ".")
                    .Select(line => line.ToCharArray()).ToArray();
            
            var result = 0;


            //print grid
            Console.WriteLine(grid.Length);
            Console.WriteLine(grid[0].Length);
            for (int x = 0; x < grid.Length; x++)
            {
                var line = grid[x];
                for (int y = 0; y < line.Length; y++)
                {
                    // Console.Write(grid[x][y]);

                    if (grid[x][y] == '@')
                    {
                        //check surroundings
                        var surroundings = new List<char>
                        {
                            grid[x - 1][y - 1], grid[x - 1][y], grid[x - 1][y + 1],
                            grid[x][y - 1],                     grid[x][y + 1],
                            grid[x + 1][y - 1], grid[x + 1][y], grid[x + 1][y + 1],
                        };

                        int count = surroundings.Count(c => c == '@');
                        if (count < 4)
                        {
                            result ++;
                            // remove center @
                            grid[x][y] = '.';
                            x = 0;
                            y = 0;
                        }
                    }
                }
            
                // Console.WriteLine();
            }
            Console.WriteLine("RESULT: " + result);
        }
    }
}