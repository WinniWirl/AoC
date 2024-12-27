using System.Text.RegularExpressions;
using AoC_Day;

namespace AoC2024_Day3
{
    class AOCProgram : AoCDay, ISolvable
    {
        private static int GetSumOfAllProducts(string input)
        {
            var regex = new Regex(@"mul\((\d{1,3})\,(\d{1,3})\)");
            return regex.Matches(input)
                        .Select(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value))
                        .Sum();
        }

        private static int GetSumOfAllProductsPart2(string input)
        {
            input = "do()" + input + "don't()";
            var regex = new Regex(@"do\(\).*?mul\(\d{1,3}\,\d{1,3}\).*?don't\(\)");
            return regex.Matches(input)
                        .Select(m => m.Value)
                        .Sum(GetSumOfAllProducts);
        }

        public void SolvePart1()
        {
            var input = string.Join("", Helper.Helper.getInputAsLinesOfCurrentDay(day));
            Console.WriteLine(GetSumOfAllProducts(input));
        }

        public void SolvePart2()
        {
            var input = string.Join("", Helper.Helper.getInputAsLinesOfCurrentDay(day));
            Console.WriteLine(GetSumOfAllProductsPart2(input));
        }
    }
}