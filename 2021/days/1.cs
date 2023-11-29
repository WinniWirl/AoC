namespace Day1
{
    class AOCProgram {
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

        public string? solvePart1(){
            Console.WriteLine($"Solution Day {day} Part 1:");
            return "???";
        }    

        public string? solvePart2(){
            Console.WriteLine($"Solution Day {day} Part 2:");
            return null;
        }  
    }
}