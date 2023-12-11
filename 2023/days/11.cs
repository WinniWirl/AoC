using AoC_Day;
using Helper;

namespace AoC2023_Day11
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<char>> matrix = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.ToCharArray().ToList()).ToList();
            matrix = ExpandHorizontal(matrix);
            matrix = ExpandVertical(matrix);
            PrintMatrix(matrix);

            List<Galaxy> galaxies = [];
            matrix.ForEachIndexed((line, lineIndex) => {
                line.ForEachIndexed((element, elementIndex) => {
                    if(element != '.') galaxies.Add(new Galaxy(elementIndex, lineIndex));
                });
            });

            long result = 0;
            while(galaxies.Count != 0)
            {
                Galaxy galaxy = galaxies[0];
                galaxies.RemoveAt(0);
                galaxies.ForEach(other => {
                    result += galaxy.DistanceTo(other);
                });
            }

            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<List<char>> matrix = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.ToCharArray().ToList()).ToList();
            List<int> emptyLines = IndexesOfEmptyLinesIn(matrix);
            List<int> emptyColumns = IndexesOfEmptyLinesIn(Transpose(matrix));
            // PrintMatrix(matrix);

            List<Galaxy> galaxies = [];
            matrix.ForEachIndexed((line, lineIndex) => {
                line.ForEachIndexed((element, elementIndex) => {
                    if(element != '.') {
                        long xPos = emptyColumns.Where(index => index < elementIndex).Count() * 999999 + elementIndex;
                        long yPos = emptyLines.Where(index => index < lineIndex).Count() * 999999 + lineIndex;
                        Console.WriteLine($"x: {xPos}, y: {yPos}");
                        galaxies.Add(new Galaxy(xPos, yPos));
                    }
                });
            });

            long result = 0;
            while(galaxies.Count != 0)
            {
                Galaxy galaxy = galaxies[0];
                galaxies.RemoveAt(0);
                galaxies.ForEach(other => {
                    result += galaxy.DistanceTo(other);
                });
            }

            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }

        public List<List<char>> Transpose(List<List<char>> matrix) {
            int rows = matrix.Count;
            int columns = matrix[0].Count;

            List<List<char>> result = [];

            for (var c = 0; c < columns; c++)
            {
                result.Add([]);
                for (var r = 0; r < rows; r++)
                {
                    result[c].Add(matrix[r][c]);
                }
            }

            return result;
        }

        public List<List<char>> ExpandVertical(List<List<char>> matrix) {
            List<List<char>> result = [];
            matrix.ForEach(line => {
                result.Add(line);
                if(!line.Contains('#')) result.Add(line);
            });
            return result;
        }

        public List<List<char>> ExpandHorizontal(List<List<char>> matrix) {
            return Transpose(ExpandVertical(Transpose(matrix)));
        }

        public List<int> IndexesOfEmptyLinesIn(List<List<char>> matrix) {
            List<int> result = [];
            matrix.ForEachIndexed((line, lineIndex) => {
                if(!line.Contains('#')) result.Add(lineIndex);
            });
            return result;
        }

        public void PrintMatrix(List<List<char>> matrix){
            matrix.ForEach(line => {
                line.ForEach(c => Console.Write(c));
                Console.Write("\n");
            });
        }
    }

    class Galaxy {
        long x;
        long y;

        public Galaxy(long x, long y){
            this.x = x;
            this.y = y;
        }

        public long DistanceTo(Galaxy other) {
            return Math.Abs(x - other.x) + Math.Abs(y - other.y);
        }
    }
}