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
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            string pattern = @"\d+";
            int result = 0;
            Dictionary<string, int> gears = [];
            //find all gears
            lines.ForEachIndexed((line, lineIndex) => {
                foreach (Match match in Regex.Matches(line, @"\*"))
                {
                    gears.Add($"{lineIndex}_{match.Index}", -1);
                }
            });


            lines.ForEachIndexed ((line, lineIndex) => 
            {
                // foreach (Match match in Regex.Matches(line, pattern))
                // {
            //         int matchIndex = match.Index;
            //         int matchLength = match.Value.Length;
            //         List<char> charsAround = [];
            //         bool isOnLineEnd = matchIndex + matchLength == line.Length;
            //         (int lineIndex, int position) topLeft = (lineIndex == 0 ? 0 : lineIndex-1, matchIndex == 0 ? 0 : matchIndex-1);
            //         (int lineIndex, int position) botRight = (lineIndex == lines.Count-1 ? lineIndex : lineIndex+1, isOnLineEnd ? matchIndex + matchLength : matchIndex + matchLength+1);

            //         //charsAround.Add();
            //         //the top ones
            //         if(lineIndex != 0)
            //             charsAround.AddRange(lines[lineIndex-1].Substring(matchIndex == 0 ? 0 : matchIndex-1, isOnLineEnd ? matchLength + 1 : matchLength + 2).ToCharArray());
            //         //left & right
            //         if (matchIndex > 0) charsAround.Add(line[matchIndex-1]);
            //         if (!isOnLineEnd) charsAround.Add(line[matchIndex+matchLength]);
            //         //the below ones
            //         if(lineIndex != line.Length-1)
            //             charsAround.AddRange(lines[lineIndex+1].Substring(matchIndex == 0 ? 0 : matchIndex-1, isOnLineEnd ? matchLength + 1 : matchLength + 2).ToCharArray());
                    
            //         charsAround.RemoveAll(item => item != '*');
            //         if(charsAround.Count != 0) {

            //         }
            //     }                        
            // });
            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }
    }
}