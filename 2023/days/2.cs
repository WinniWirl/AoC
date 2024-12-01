using System.Text.RegularExpressions;
using AoC_Day;

namespace AoC2023_Day2
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            int sum = 0;            
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            foreach (string line in lines)
            {
                string pattern = @"\d+";
                int gameId = int.Parse(Regex.Match(line, pattern).Value);
                if (checkForPossibleGame(line)) sum += gameId;
            }
            Console.WriteLine($"Die Lösung für Part 1 ist: {sum}");
        }

        public void SolvePart2()
        {
            int sum = 0;            
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            foreach (string line in lines)
            {
                sum += calcPowerOfGame(line);
            }
            Console.WriteLine($"Die Lösung für Part 1 ist: {sum}");
        }

        public bool checkForPossibleGame(string game){
        string[] pulls = game.Split(":")[1].Split(";");
        foreach (string pull in pulls)
            {
                string pattern = @"(\d+) green";
                int greens = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out greens);
                if (greens > 13) return false;

                pattern = @"(\d+) red";
                int reds = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out reds);
                if (reds > 12) return false;

                pattern = @"(\d+) blue";
                int blues = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out blues);
                if (blues > 14) return false;
            }
            return true;
        }

        public int calcPowerOfGame(string game){
        string[] pulls = game.Split(":")[1].Split(";");
        int maxGreen = 0;
        int maxRed = 0;
        int maxBlue = 0;
        foreach (string pull in pulls)
            {
                string pattern = @"(\d+) green";
                int greens = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out greens);
                maxGreen = int.Max(maxGreen, greens);

                pattern = @"(\d+) red";
                int reds = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out reds);
                maxRed = int.Max(maxRed, reds);


                pattern = @"(\d+) blue";
                int blues = 0;
                int.TryParse(Regex.Match(pull, pattern).Value.Split(" ")[0], out blues);
                maxBlue = int.Max(maxBlue, blues);
            }
            return maxBlue * maxGreen * maxRed;
        }


    }
}