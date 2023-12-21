using AoC_Day;

namespace AoC2023_Day13
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<List<char>>> inputAsCharArray = Helper.Helper.getInputAsGroupOfCurrentDay(day).Select(group => group.Select(line => line.ToCharArray().ToList()).ToList()).ToList();
            int result = 0;
            foreach (List<List<char>> groupOrig in inputAsCharArray)
            {
                bool isHorizontal = true;
                int mirrowAfter = -1;
                List<List<char>> group = groupOrig;
                for (int line = 0; line < group.Count-1; line++)
                {
                    if (checkIfIsMirrowLine(group, line)) 
                    {
                        Console.WriteLine($"FOUND AT LINE {line}");
                        mirrowAfter = line;
                        break;
                    }
                }
                if(mirrowAfter == -1)
                {
                    group = Transpose(group);
                    isHorizontal = false;
                    for (int column = 0; column < group.Count-1; column++)
                    {
                        if (checkIfIsMirrowLine(group, column)) 
                        {
                            Console.WriteLine($"FOUND AT COLUMN {column}");
                            mirrowAfter = column;
                            break;
                        }
                    }
                }
                result += (mirrowAfter+1) * (isHorizontal ? 100 : 1);
            }
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public bool checkIfIsMirrowLine(List<List<char>> matrix, int checkFromPosition){
            bool result = true;
            // Console.WriteLine($"{checkFromPosition}: Checking {new string(matrix[checkFromPosition+1].ToArray())} vs {new string(matrix[checkFromPosition].ToArray())}");
            for (int i = 0; i < matrix.Count; i++)
            {
                if(checkFromPosition +1 + i >= matrix.Count || checkFromPosition - i < 0) return result;
                if(!new string(matrix[checkFromPosition+1+i].ToArray()).Equals(new string(matrix[checkFromPosition-i].ToArray()))) return false;
                // Console.WriteLine("Checking further");
            }
            return false;
        }

        public bool checkIfIsMirrowButOneWrongIsOkay(List<List<char>> matrix, int checkFromPosition){
            bool result = true;
            int wrongsAllowed = 1;
            // Console.WriteLine($"{checkFromPosition}: Checking {new string(matrix[checkFromPosition+1].ToArray())} vs {new string(matrix[checkFromPosition].ToArray())}");
            for (int i = 0; i < matrix.Count; i++)
            {
                if(checkFromPosition +1 + i >= matrix.Count || checkFromPosition - i < 0) return result;
                for (int c = 0; c < matrix[0].Count; c++)
                {
                    if(matrix[checkFromPosition+1+i][c] != matrix[checkFromPosition-i][c]){
                        if(wrongsAllowed > 0) 
                        {
                            wrongsAllowed--;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                }
                if(!new string(matrix[checkFromPosition+1+i].ToArray()).Equals(new string(matrix[checkFromPosition-i].ToArray()))) 
                {
                    return false;
                }
                // Console.WriteLine("Checking further");
            }
            return false;
        }

        public void SolvePart2()
        {
                        List<List<List<char>>> inputAsCharArray = Helper.Helper.getInputAsGroupOfCurrentDay(day).Select(group => group.Select(line => line.ToCharArray().ToList()).ToList()).ToList();
            int result = 0;
            foreach (List<List<char>> groupOrig in inputAsCharArray)
            {
                bool isHorizontal = true;
                int mirrowAfter = -1;
                List<List<char>> group = groupOrig;
                for (int line = 0; line < group.Count-1; line++)
                {
                    if (checkIfIsMirrowButOneWrongIsOkay(group, line)) 
                    {
                        Console.WriteLine($"FOUND AT LINE {line}");
                        mirrowAfter = line;
                        break;
                    }
                }
                if(mirrowAfter == -1)
                {
                    group = Transpose(group);
                    isHorizontal = false;
                    for (int column = 0; column < group.Count-1; column++)
                    {
                        if (checkIfIsMirrowButOneWrongIsOkay(group, column)) 
                        {
                            Console.WriteLine($"FOUND AT COLUMN {column}");
                            mirrowAfter = column;
                            break;
                        }
                    }
                }
                result += (mirrowAfter+1) * (isHorizontal ? 100 : 1);
            }
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
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

    }
}