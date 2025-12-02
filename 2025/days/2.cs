using System.Text.RegularExpressions;
using AoC_Day;

namespace AoC2025_Day2
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0];
            var parts = rawInput.Split(',');
            var result = 0L;
            foreach (var part in parts)
            {
                var numbers = part.Split("-");
                var a = long.Parse(numbers[0]);
                var b = long.Parse(numbers[1]);

                for (var number = a; number <= b; number++)
                {
                    var numStr = number.ToString();
                    if (numStr.Length % 2 != 0) continue;

                    var firstHalf = numStr.Substring(0, numStr.Length / 2);
                    var secondHalf = numStr.Substring(numStr.Length / 2);
                    if (firstHalf.Equals(secondHalf))
                    {
                        result += number;
                    }
                }
            }
            Console.WriteLine(result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0];
            var result =
                rawInput.Split(",")
                    .Select(input => input.Split("-"))
                    .SelectMany(range => NumbersInRange(range[0], range[1]))
                    .Where(number => Regex.IsMatch(number.ToString(), @"^(\d+)(\1)+$"))
                    .Sum();

            Console.WriteLine(result);
        }

        private static IEnumerable<long> NumbersInRange(string start, string end)
        {
            for (var n = long.Parse(start); n <= long.Parse(end); n++)
                yield return n;
        }


        public void SolvePart2_no_LINQ()
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
                        result += number;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}