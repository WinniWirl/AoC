using System.Text.RegularExpressions;
using AoC_Day;
using Helper;

namespace AoC2023_Day12
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string[]> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(l => l.Split(" ")).ToList();
            int result = 0;

            foreach (string[] line in input)
            {
                List<int> commands = line[1].Split(",").Select(int.Parse).ToList();
                result += generateAllPossibleCombinationsFor(line[0]).Where(comb => matchesCommand(comb, commands)).Count();
            }

            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public bool matchesCommand(string input, List<int> commands){
            string[] groupsOfHashtags = input.Split(".", StringSplitOptions.RemoveEmptyEntries);
            if(groupsOfHashtags.Length != commands.Count) return false;
            for (int i = 0; i < groupsOfHashtags.Length; i++)
            {
                if(groupsOfHashtags[i].Length != commands[i]) return false;
            }
            return true;
        }

        public List<string> generateAllPossibleCombinationsFor(string input){
            Console.WriteLine($"generating variant for: {input}");

            List<string> result = [];
            result.Add(input);
            
            while(result[0].Contains('?')){
                List<string> tmp = [];
                for (int i = 0; i < result.Count; i++)
                {
                    Match m = Regex.Match(result[i], @"\?");
                    char[] chars = result[i].ToCharArray();
                    chars[m.Index] = '.';
                    tmp.Add(new string(chars));
                    chars[m.Index] = '#';
                    tmp.Add(new string(chars));
                }
                result = tmp;
            }
            return result;
        }

        public void SolvePart2()
        {
            List<string[]> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(l => l.Split(" ")).ToList();
            //unfolding
            input.ForEach(item => {
                item[0] = item[0] + "?" + item[0] + "?" + item[0] + "?" + item[0] + "?" + item[0];
                item[1] = item[1] + "," + item[1] + "," + item[1] + "," + item[1] + "," + item[1];
            });  

            input.ForEach(i => Console.WriteLine($"{i[0]} - {i[1]}"));     

            input.ForEach(i => getAllPossibleSpringPatters(i[1], i[0].Length));     
        }

        public bool isInWhitelistPattern(string whitelistPattern, string springPattern){
            if(whitelistPattern.Length != springPattern.Length) {
                Console.WriteLine("WARNING! SHOULD NOT HAPPEN!");
                return false;
            }
            for (int i = 0; i < whitelistPattern.Length; i++)
            {
                if(whitelistPattern[i] != '?' && whitelistPattern[i] != springPattern[i]) return false; 
            }
            return true;
        }

        public List<string> getAllPossibleSpringPatters(string rawPattern, int whitelistPatternLength){
            List<int> commands = rawPattern.Split(",").Select(int.Parse).ToList();
            int dotsToInsert = whitelistPatternLength - (commands.Count - 1) - commands.Sum();
            Console.WriteLine($"Dots to insert available: {dotsToInsert}");

            return [];
        }

        private List<string> appendSpringsFromPattern(string initialSpringPattern, List<int> commands, int dotsToInsert)
        {
            // List<string> springs = new List<string>();
            // for (int i = 0; i < dotsToInsert; i++)
            // {
            //     springs.Add()
            //     springs.AddRange(appendSpringsFromPattern(initialSpringPattern, commands, dotsToInsert));
            // }     
            return [];       
        }
    }
}