using System.Text.RegularExpressions;
using AoC_Day;
using Helper;

namespace AoC2023_Day4
{
    class AOCProgram : AoCDay, ISolvable {
        private string _day = "0";

        public string day {
            get { return _day; }
            set {
                _day = value;
                if (!day.Equals(DateTime.Today.Day.ToString())){
                    Console.WriteLine("WARNING: Todays date does not equal your puzzles.");
                }
            }
        }

        public void SolvePart1(){
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            int result = 0;
            
            foreach (string line in lines)
            {
                Scratchcard sc = new Scratchcard(line);
                result += sc.getScore();
            }
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }    

        public void SolvePart2(){
            List<string> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            List<Scratchcard> scratchcards = [];
            int result = 0;
            foreach (string line in lines)
            {
                scratchcards.Add(new Scratchcard(line));
            }

            scratchcards.ForEachIndexed((sc, index) => {
                int matches = sc.getMatches();
                for (int i = 1; i <= matches; i++)
                {
                    scratchcards[index + i].addCopy(sc.copys);
                    Console.WriteLine($"[{index}] Added {sc.copys} to scratchcard[{index + i}]");
                }
                result += sc.copys;
            });

            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }  
    }

    class Scratchcard{
        List<int> winningNumbers = [];
        List<int> myNumbers = [];

        public int copys {get; set;} = 1;
        
        public Scratchcard(string input){
                string[] splitted = input.Split(":")[1].Split("|", StringSplitOptions.RemoveEmptyEntries); 
                string[] winningNumbers = splitted[0].Split(" ",  StringSplitOptions.RemoveEmptyEntries);
                this.winningNumbers = winningNumbers.Select(int.Parse).ToList();
                string[] myNumbers = splitted[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                this.myNumbers = myNumbers.Select(int.Parse).ToList();
        }

        public int getMatches(){
            this.winningNumbers.RemoveAll(number => !this.myNumbers.Contains(number));
            return this.winningNumbers.Count;
        }

        public int getScore(){
            this.winningNumbers.RemoveAll(number => !this.myNumbers.Contains(number));
            return Convert.ToInt32(this.winningNumbers.Count > 0 ? Math.Pow(2.0, this.winningNumbers.Count -1) : 0);
        }

        public void addCopy(int amount){
            this.copys += amount;
        }
    }
}