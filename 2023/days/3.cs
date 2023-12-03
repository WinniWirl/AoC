using System.Text.RegularExpressions;
using AoC_Day;
using Helper;

namespace AoC2023_Day3
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            string pattern = @"\d+";
            int result = 0;
            lines.ForEachIndexed ((line, lineIndex) => 
            {
                foreach (Match match in Regex.Matches(line, pattern))
                {
                    int matchIndex = match.Index;
                    int matchLength = match.Value.Length;
                    List<char> charsAround = [];
                    
                    //charsAround.Add();
                    bool isOnLineEnd = matchIndex + matchLength == line.Length;
                    //the top ones
                    if(lineIndex != 0)
                        charsAround.AddRange(lines[lineIndex-1].Substring(matchIndex == 0 ? 0 : matchIndex-1, isOnLineEnd ? matchLength + 1 : matchLength + 2).ToCharArray());
                    //left & right
                    if (matchIndex > 0) charsAround.Add(line[matchIndex-1]);
                    if (!isOnLineEnd) charsAround.Add(line[matchIndex+matchLength]);
                    //the below ones
                    if(lineIndex != line.Length-1)
                        charsAround.AddRange(lines[lineIndex+1].Substring(matchIndex == 0 ? 0 : matchIndex-1, isOnLineEnd ? matchLength + 1 : matchLength + 2).ToCharArray());
                    
                    charsAround.RemoveAll(item => item == '.');
                    if(charsAround.Count != 0) result += int.Parse(match.Value);
                }                        
            });
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<string> linesRaw = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            //optimize list
            List<string> lines = [];
            lines.Add(new string('.', linesRaw[0].Length + 2));
            foreach (string lineRaw in linesRaw) { lines.Add("." + lineRaw + "."); }
            lines.Add(new string('.', lines[0].Length));
            
            string pattern = @"\d+";
            int result = 0;
            Dictionary<string, int> gears = [];

            lines.ForEachIndexed ((line, lineIndex) => 
            {
                foreach (Match match in Regex.Matches(line, pattern))
                {
                    int matchIndex = match.Index;

                    //detect position of *
                    List<char> charsAround = [];
                    for (int i = -1; i <= 1; i++) {
                        for (int j = 0; j < match.Value.Length + 2; j++)
                        {
                            if(lines[lineIndex + i][matchIndex - 1 + j] != '*') continue;
                            
                            string idOfGear = $"pos{lineIndex + i}line{matchIndex - 1 + j}";
                            if(!gears.TryAdd(idOfGear, int.Parse(match.Value))){
                            result += gears[idOfGear] * int.Parse(match.Value);
                            }
                        }
                    }
                }
            });
            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }
    }
}