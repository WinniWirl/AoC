using System.Collections;
using AdventOfCode;
using AoC2023_Day18;

namespace Helper
{
    static class Helper {

        /**
        *   an indexed foreach.
        */
        public static void ForEachIndexed<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

        public static List<string> getInputAsLinesOfCurrentDay(string day)
        {
            StreamReader? sr = null;
            List<string> result = [];
            try
            {
                sr = new StreamReader($".\\{AdventOfCode.AdventOfCode.year}\\inputs\\day{day}.txt");
                string? line = sr.ReadLine();
                // Continue to read until you reach end of file
                while (line != null)
                {
                    result.Add(line);
                    line = sr.ReadLine();
                }
            } catch(FileNotFoundException)
            {
                Console.WriteLine($"Input of day {day} not Found!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                sr?.Close();
            }
            
            return result;
        }

        public static List<List<string>> getInputAsGroupOfCurrentDay(string day){
            List<string> lines = getInputAsLinesOfCurrentDay(day);
            List<string> tmp = [];
            List<List<string>> groups = [];
            
            foreach (string line in lines)
            {
                if(line.Length == 0) {
                    groups.Add(new List<string>(tmp));
                    tmp = [];
                    continue;
                }
                tmp.Add(line);
            }
            groups.Add(new List<string>(tmp));
            return groups;
        }

        public static List<GridElement> floodFill(List<GridElement> grid, (int x, int y) position)
        {
            int minX = grid.Min(t => t.position.x);
            int maxX = grid.Max(t => t.position.x);
            int minY = grid.Min(t => t.position.y);
            int maxY = grid.Max(t => t.position.y);

            var queue = new Queue<GridElement>();
            queue.Enqueue(new GridElement(position));
            while(queue.Count > 0)
            {
                // if(queue.Count % 100 == 0) Console.WriteLine($"Queue size: {queue.Count}");
                GridElement element = queue.Dequeue();
                if(element.position.x < minX || element.position.x >= maxX || element.position.y < minY || element.position.y >= maxY) continue;
                if(grid.Contains(element)) continue;
                grid.Add(element);
                queue.Enqueue(new GridElement(element.positionOfTileIn(Direction.NORTH)));
                queue.Enqueue(new GridElement(element.positionOfTileIn(Direction.SOUTH)));
                queue.Enqueue(new GridElement(element.positionOfTileIn(Direction.WEST)));
                queue.Enqueue(new GridElement(element.positionOfTileIn(Direction.EAST)));
            }
            return grid;
        }

        public static List<GridElement> fillInner(List<GridElement> grid){
            int minX = grid.Min(t => t.position.x);
            int maxX = grid.Max(t => t.position.x);
            int minY = grid.Min(t => t.position.y);
            int maxY = grid.Max(t => t.position.y);
            

            for (int y = minY; y < maxY; y++)
            {
                bool isInside = false;
                bool lastWasBorder = false;
                for (int x = minX; x < maxX; x++)
                {
                    GridElement element = new GridElement((x, y));
                    if(grid.Contains(element)) {
                        if(!lastWasBorder) {
                            isInside = !isInside;
                            lastWasBorder = true;
                        }
                    }
                    else 
                    {
                        lastWasBorder = false;
                        if(isInside) grid.Add(element);
                    }
                }
            }

            return grid;
        }
    }

    class GridElement{
        public (int x, int y) position;

        public (int x, int y) positionOfTileIn(Direction direction) {
            switch (direction)
            {
                case Direction.NORTH: return (position.x, position.y - 1);
                case Direction.EAST: return (position.x + 1, position.y);
                case Direction.SOUTH: return (position.x, position.y + 1);
                case Direction.WEST: return (position.x - 1, position.y);
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public GridElement((int x, int y) position){
            this.position = position;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            GridElement other = (GridElement)obj;
            if (position.x != other.position.x || position.y != other.position.y) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 23 * position.x.GetHashCode();
                hash = hash * 23 * position.y.GetHashCode();
                return hash;
            }
        }
    }

    enum Direction {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    static class Matrix<T>
    {
        public static T[][] TransposeMatrix(T[][] matrix)
        {
            var rows    = matrix.Length;
            var columns = matrix[0].Length;

            var result = new T[columns][];

            for (var c = 0; c < columns; c++)
            {
                result[c] = new T[5];
                for (var r = 0; r < rows; r++)
                {
                    result[c][r] = matrix[r][c];
                }
            }

            return result;
        }
    }
}
