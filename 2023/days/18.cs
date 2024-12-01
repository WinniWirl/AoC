using AoC_Day;
using Helper;

namespace AoC2023_Day18
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<string>> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.Split(" ").ToList()).ToList();
            List<Position> tiles = [];
            Position currentTile = new Position(0,0);
            foreach (List<string> line in input)
            {
                for (int i = 0; i < int.Parse(line[1]); i++)
                {
                    Helper.Direction direction = Helper.Direction.NORTH;
                    switch (line[0])
                    {
                        case "U": {direction = Helper.Direction.NORTH; break;}
                        case "D": {direction = Helper.Direction.SOUTH; break;}
                        case "L": {direction = Helper.Direction.WEST; break;}
                        case "R": {direction = Helper.Direction.EAST; break;}
                        default: throw new ArgumentOutOfRangeException();
                    } 
                    currentTile = currentTile.positionIn(direction);
                    tiles.Add(currentTile);
                }
            }
            // PrintGrid(tiles.ConvertAll(x => (GridElement)x));
            List<Position> gridElements = Helper.Helper.floodFill(tiles, new Position(1, 1));
            // List<GridElement> gridElements = Helper.Helper.fillInner(tiles.ConvertAll(x => (GridElement)x));
            // Console.WriteLine("\n----------------------");
            // PrintGrid(gridElements);
            Console.WriteLine($"Solution Day {day} Part 1: {gridElements.Count}");

        }

        public void PrintGrid(List<Position> tiles){
            int minX = tiles.Min(t => t.x);
            int maxX = tiles.Max(t => t.x);
            int minY = tiles.Min(t => t.y);
            int maxY = tiles.Max(t => t.y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Console.Write(tiles.Contains(new Position(x, y)) ? "#" : " ");   
                }
                Console.Write("\n");
            }
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}