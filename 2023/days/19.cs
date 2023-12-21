using System.Text.RegularExpressions;
using AoC_Day;

namespace AoC2023_Day19
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<string>> input = Helper.Helper.getInputAsGroupOfCurrentDay(day);
            
            //init parts
            List<Part> parts = [];
            foreach (string partDescription in input[1])
            {
                parts.Add(new Part(partDescription));
            }

            //init workflows
            Dictionary<string, List<string>> workflows = [];
            foreach (string workflowDescriptions in input[0]){
                var split = workflowDescriptions.Replace("}", "{").Split('{', StringSplitOptions.RemoveEmptyEntries);
                workflows.Add(split.First(), split.Last().Split(",").ToList());            
            }

            int result = 0;

            foreach (Part part in parts)
            {
                string currentWorkflowKey = "in";
                while(!currentWorkflowKey.Equals("R")){
                    List<string> workflow = workflows[currentWorkflowKey];
                    for (int i = 0; i < workflow.Count; i++)
                    {
                        string[] splitAtDoublepoint = workflow[i].Split(":");
                        //no condition
                        if(splitAtDoublepoint.Length == 1) {
                            currentWorkflowKey = splitAtDoublepoint[0];
                            break;
                        }
                        if(matchingCondition(part, splitAtDoublepoint[0])) {
                            currentWorkflowKey = splitAtDoublepoint[1];
                            break;
                        }
                    }
                    if(currentWorkflowKey == "A") {
                        result += part.getValue();
                        break;
                    }
                }
            }

            Console.WriteLine($"Solution Day {day} Part 1: {result}");

        }


        public bool matchingCondition(Part part, string condition) {
            int value = int.Parse(condition.Split('>','<')[1]);
            switch (condition[0]){
                case 'x': { if (Regex.IsMatch(condition, @">") ? part.x > value : part.x < value) return true; break; }
                case 'm': { if (Regex.IsMatch(condition, @">") ? part.m > value : part.m < value) return true; break; }
                case 'a': { if (Regex.IsMatch(condition, @">") ? part.a > value : part.a < value) return true; break; }
                case 's': { if (Regex.IsMatch(condition, @">") ? part.s > value : part.s < value) return true; break; }
            }

            return false;
        }



        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }

    class Part{
        public int x, m, a, s;

        public Part(string description) {
            string pattern = @"\d+";
            List<Match> matches = Regex.Matches(description, pattern).ToList();
            x = int.Parse(matches[0].Value);
            m = int.Parse(matches[1].Value);
            a = int.Parse(matches[2].Value);
            s = int.Parse(matches[3].Value);
        }

        public int getValue(){
            return x+m+a+s;
        }
    }
}