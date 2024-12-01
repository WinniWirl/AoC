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

        public static List<Position> floodFill(List<Position> borders, Position position)
        {
            int minX = borders.Min(t => t.x);
            int maxX = borders.Max(t => t.x);
            int minY = borders.Min(t => t.y);
            int maxY = borders.Max(t => t.y);

            var queue = new Queue<Position>();
            queue.Enqueue(position);
            while(queue.Count > 0)
            {
                // if(queue.Count % 100 == 0) Console.WriteLine($"Queue size: {queue.Count}");
                Position element = queue.Dequeue();
                if(element.x < minX || element.x >= maxX || element.y < minY || element.y >= maxY) continue;
                if(borders.Contains(element)) continue;
                borders.Add(element);
                queue.Enqueue(position.North());
                queue.Enqueue(position.South());
                queue.Enqueue(position.East());
                queue.Enqueue(position.West());
            }
            return borders;
        }

        public static List<Position> fillInner(List<Position> borders){
            int minX = borders.Min(t => t.x);
            int maxX = borders.Max(t => t.x);
            int minY = borders.Min(t => t.y);
            int maxY = borders.Max(t => t.y);
            

            for (int y = minY; y < maxY; y++)
            {
                bool isInside = false;
                bool lastWasBorder = false;
                for (int x = minX; x < maxX; x++)
                {
                    Position element = new Position(x, y);
                    if(borders.Contains(element)) {
                        if(!lastWasBorder) {
                            isInside = !isInside;
                            lastWasBorder = true;
                        }
                    }
                    else 
                    {
                        lastWasBorder = false;
                        if(isInside) borders.Add(element);
                    }
                }
            }

            return borders;
        }
    }

    class GridElement<T>{
        public Position position;

        public T data;

        public GridElement(T data, (int x, int y) position){
            this.position = new Position(position.x, position.y);
            this.data = data;
        }

        public GridElement(T data, Position position) {
            this.position = position;
            this.data = data;
        }

        public GridElement(T data, int x, int y) {
            this.position = new Position(x, y);
            this.data = data;
        }
    }

    class Position3{
        public int x, y, z;

        public Position3 (int x, int y, int z){
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    class Position{
        public int x; 
        public int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position North() { return new Position(x, y-1);}
        public Position South() { return new Position(x, y+1);}
        public Position East() { return new Position(x+1, y);}
        public Position West() { return new Position(x-1, y);}

        public Position positionIn( Direction direction) {
            switch (direction)
            {
                case Direction.NORTH: return North();
                case Direction.EAST: return East();
                case Direction.SOUTH: return South();
                case Direction.WEST: return West();
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Position other = (Position)obj;
            if (x != other.x || y != other.y) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 23 * x.GetHashCode();
                hash = hash * 23 * y.GetHashCode();
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
