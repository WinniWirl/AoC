using System.Text.RegularExpressions;
using AoC_Day;
using Helper;

namespace AoC2023_Day1
{
    class AOCProgram : AoCDay, ISolvable {

        public void SolvePart1(){
            string pattern = @"\d";
            int sum = 0;
            
            foreach (string line in Helper.Helper.getInputAsLinesOfCurrentDay(day))
            {
                string firstNumber = "?";
                string lastNumber = "?";
                foreach (Match match in Regex.Matches(line, pattern))
                {
                    if(firstNumber == "?") {
                        firstNumber = match.Value;
                    }
                    lastNumber = match.Value;
                } 
                Console.WriteLine(firstNumber + lastNumber);
                sum += int.Parse(firstNumber + lastNumber);
            }


            Console.WriteLine($"Solution Day {day} Part 1: {sum}");
        }    

        public void SolvePart2(){
            string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";
            int sum = 0;
            
            foreach (string line in Helper.Helper.getInputAsLinesOfCurrentDay(day))
            {
                int firstNumber = -1;
                int lastNumber = -1;
                Match match = Regex.Match(line, pattern);
                if(firstNumber == -1) {
                    bool success = int.TryParse(match.Value, out firstNumber);
                    if (!success) firstNumber = writtenToNumber(match.Value);
                }

                match = Regex.Match(line, pattern, RegexOptions.RightToLeft);
                if(lastNumber == -1) {
                    bool success = int.TryParse(match.Value, out lastNumber);
                    if (!success) lastNumber = writtenToNumber(match.Value);
                }
                
                sum += firstNumber * 10 + lastNumber;
            }


            Console.WriteLine($"Solution Day {day} Part 2: {sum}");
        }

        public int writtenToNumber(string number){
            switch (number)
            {
                case "one": return 1; 
                case "two": return 2; 
                case "three": return 3; 
                case "four": return 4; 
                case "five": return 5; 
                case "six": return 6; 
                case "seven": return 7; 
                case "eight": return 8; 
                case "nine": return 9; 
                default:
                Console.WriteLine("WARNING!!! " + number); 
                return 0; 
            }
        }  
    }
}