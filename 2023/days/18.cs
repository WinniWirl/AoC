using System.Drawing;
using AoC_Day;
using Helper;

namespace AoC2023_Day18
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<string>> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.Split(" ").ToList()).ToList();
            List<Tile> tiles = [];
            Tile currentTile = new Tile((0,0));
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
                    currentTile = new Tile(currentTile.positionOfTileIn(direction));
                    tiles.Add(currentTile);
                }
            }
            // PrintGrid(tiles.ConvertAll(x => (GridElement)x));
            List<GridElement> gridElements = Helper.Helper.floodFill(tiles.ConvertAll(x => (GridElement)x), (1, 1));
            // List<GridElement> gridElements = Helper.Helper.fillInner(tiles.ConvertAll(x => (GridElement)x));
            // Console.WriteLine("\n----------------------");
            // PrintGrid(gridElements);
            Console.WriteLine($"Solution Day {day} Part 1: {gridElements.Count}");

        }

        public void PrintGrid(List<GridElement> tiles){
            int minX = tiles.Min(t => t.position.x);
            int maxX = tiles.Max(t => t.position.x);
            int minY = tiles.Min(t => t.position.y);
            int maxY = tiles.Max(t => t.position.y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Console.Write(tiles.Contains(new Tile((x, y))) ? "#" : " ");   
                }
                Console.Write("\n");
            }
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }

class Tile : GridElement{
        public bool isDigged = false;
        public Color color;

        public Tile((int x, int y) position): base(position) {
            
        }
    }
}