namespace AoC_Day
{
    public class AoCDay
    {
        private string _day = "0";

        public string day
        {
            get { return _day; }
            set
            {
                _day = value;
                if (!day.Equals(DateTime.Today.Day.ToString()))
                {
                    Console.WriteLine("WARNING: Todays date does not equal your puzzles.");
                }
            }
        }
    }
}

interface ISolvable
{
    public void SolvePart1();
    public void SolvePart2();
}