using AoC_Day;
using Helper;

namespace AoC2025_Day9
{
    class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var tiles = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(line => new Tile(line))
                .ToList();

            var tileCombinations = new List<TileCombination>();

            for (var i = 0; i < tiles.Count; i++)
            {
                for (var j = i + 1; j < tiles.Count; j++)
                {
                    tileCombinations.Add(new TileCombination(tiles[i], tiles[j]));
                }
            }

            var maxVolume = tileCombinations
                .Select(tc => tc.Volume())
                .Max();

            Console.WriteLine($"Result: {maxVolume}");
            
            // 2147478556
            // 2147427810
        }

        // public void SolvePart2_runs_forever()
        // {
        //     var corners = Helper.Helper.getInputAsLinesOfCurrentDay(day)
        //         .Select(line => new Tile(line))
        //         .Select(t => new Position(t.x, t.y))
        //         .ToList();
        //     
        //     var borders = new List<Position>();
        //
        //     for (var i = 0; i < corners.Count; i++)
        //     {
        //         var nextIndex = (i + 1) % corners.Count;
        //         var linePositions = GetLinePositions(corners[i], corners[nextIndex]);
        //         borders.AddRange(linePositions);
        //     }
        //     
        //     var filledPositions = Helper.Helper.fillInner(borders);
        //     
        //     PrintGrid(filledPositions);
        //     
        //     var tileCombinations = new List<TileCombination>();
        //
        //     for (var i = 0; i < corners.Count - 2; i++ )
        //     {
        //         var tileCombination = new TileCombination(corners[i], corners[i + 2]);
        //         // now i need to get the positions of the other two corners
        //         var calculatedCorners = new List<Position>
        //         {
        //             new Position(corners[i].x, corners[i + 2].y),
        //             new Position(corners[i + 2].x, corners[i].y)
        //         };
        //         if (calculatedCorners.All(cc => filledPositions.Contains(cc)))
        //         {
        //             tileCombinations.Add(tileCombination);
        //         }
        //     }
        //
        //     var maxVolume = tileCombinations
        //         .Select(tc => tc.Volume())
        //         .Max();
        //     
        //     var tile = tileCombinations.First(t => t.Volume() == maxVolume);
        //     Console.WriteLine($"Tiles: ({tile.Tile1.x},{tile.Tile1.y}) and ({tile.Tile2.x},{tile.Tile2.y})");
        //
        //     Console.WriteLine($"Result: {maxVolume}");
        //     
        //     // 186567246
        // }
        
        public void SolvePart2() 
        {
            var corners = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(line => new Tile(line))
                .ToList();
            
            var xValues = corners.Select(c => c.x).Order().ToList();
            var yValues = corners.Select(c => c.y).Order().ToList();
            
            
        }        
        private List<Tile> GetLinePositions(Tile start, Tile end)
        {
            var tiles = new List<Tile>();
            var dx = Math.Sign(end.x - start.x);
            var dy = Math.Sign(end.y - start.y);
            var length = Math.Max(Math.Abs(end.x - start.x), Math.Abs(end.y - start.y));
            
            for (var i = 0; i <= length; i++)
            {
                var x = start.x + i * dx;
                var y = start.y + i * dy;
                tiles.Add(new Tile(x, y));
            }

            return tiles;
        }
        
        public void PrintGrid(List<Position> tiles){
            var minX = tiles.Min(t => t.x);
            var maxX = tiles.Max(t => t.x);
            var minY = tiles.Min(t => t.y);
            var maxY = tiles.Max(t => t.y);

            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    Console.Write(tiles.Contains(new Position(x, y)) ? "#" : ".");   
                }
                Console.Write("\n");
            }
        }
    }
    
    internal class TileCombination
    {
        public Tile Tile1 { get; }
        public Tile Tile2 { get; }

        public TileCombination(Tile tile1, Tile tile2)
        {
            Tile1 = tile1;
            Tile2 = tile2;
        }

        public TileCombination(Position tile1, Position tile2)
        {
            Tile1 = new Tile((int)tile1.x, (int)tile1.y);
            Tile2 = new Tile((int)tile2.x, (int)tile2.y);
        }

        public long Volume()
        {
            var dx = Math.Abs(Tile1.x - Tile2.x) + 1;
            var dy = Math.Abs(Tile1.y - Tile2.y) + 1;
            return dx * dy;
        }
    }

    internal class Tile
    {
        public readonly long x, y;
        
        public Tile(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public Tile(string tile)
        {
            var splited = tile.Split(',');
            x = long.Parse(splited[0]);
            y = long.Parse(splited[1]);
        }
    }
}