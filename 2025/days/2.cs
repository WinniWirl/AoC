using System.Text.RegularExpressions;
using AoC_Day;

namespace AoC2025_Day2
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0];
            string[] parts = rawInput.Split(',');
            var result = 0L;
            foreach (string part in parts)
            {
                string[] numbers = part.Split("-");
                long a = long.Parse(numbers[0]);
                long b = long.Parse(numbers[1]);

                for (long number = a; number <= b; number++)
                {
                    string numStr = number.ToString();
                    if (numStr.Length % 2 != 0) continue;
                    
                    string firstHalf = numStr.Substring(0, numStr.Length / 2);
                    string secondHalf = numStr.Substring(numStr.Length / 2);
                    if (firstHalf.Equals(secondHalf))
                    {
                        result+=number;
                    }
                }
            }
            Console.WriteLine(result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0];
            string[] parts = rawInput.Split(',');
            var result = 0L;
            foreach (string part in parts)
            {
                string[] numbers = part.Split("-");
                long a = long.Parse(numbers[0]);
                long b = long.Parse(numbers[1]);

                for (long number = a; number <= b; number++)
                {
                    if (Regex.IsMatch(number.ToString(), "^(\\d+)(\\1)+$"))
                    {
                        result+=number;
                    }
                }
            }
            Console.WriteLine(result);
        }
    }
}