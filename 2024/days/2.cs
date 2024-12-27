using AoC_Day;

namespace AoC2024_Day2
{
    class AOCProgram : AoCDay, ISolvable
    {
        private int[][] getInput()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            return rawInput.Select(i => i.Split(" ").Select(Int32.Parse).ToArray()).ToArray();
        }
        private static bool IsSave(int[] line)
        {
            var indicator = line[0] < line[1] ? 1 : -1;

            for (int i = 1; i < line.Length; i++)
            {
                var differsBy = (line[i] - line[i - 1]) * indicator;
                if (differsBy is > 0 and <= 3)
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        private static bool IsSavePart2(int[] input)
        {
            if (IsSave(input)) return true;
            
            for (int i = 0; i < input.Length; i++)
            {
                var copy = input.ToList();
                copy.RemoveAt(i);
                if (IsSave(copy.ToArray()))
                {
                    return true;
                }    
            }
            
            return false;
        }

        public void SolvePart1()
        {
            var input = getInput();
            var result = input.Count(IsSave);

            Console.WriteLine(result);
        }
        
        public void SolvePart2()
        {
            var input = getInput();
            var result = input.Count(IsSavePart2);

            Console.WriteLine(result);
        }
    }
}