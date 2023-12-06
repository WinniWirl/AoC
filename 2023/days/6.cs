using AoC_Day;
using Helper;

namespace AoC2023_Day6
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            List<int> times = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
            List<int> distances = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
            Console.WriteLine(times.Count);

            List<int> result = [];
            times.ForEachIndexed((racetime, raceId) => {
                int winnable = 0;
                for (int tryWithSecondsHold = 0; tryWithSecondsHold < racetime; tryWithSecondsHold++)
                {
                    Console.WriteLine($"Check hold for {tryWithSecondsHold} is faster then {racetime}");
                    if(BeatsTime(tryWithSecondsHold, racetime, distances[raceId])) winnable++;
                }
                result.Add(winnable);
            });

            int finalResult = 1;
            result.ForEach(x => finalResult = finalResult * x);
            Console.WriteLine($"Solution Day {day} Part 1: {finalResult}");
        }

        public bool BeatsTime(long secondsHold, long raceTime, long distanceToBeat){
            return CalcDistance(secondsHold, raceTime) > distanceToBeat;
        }

        public long CalcDistance(long secondsHold, long raceTime) {
            long timeLeft = raceTime - secondsHold;
            long result = 0;
            while(timeLeft > 0) {
                result += secondsHold;
                timeLeft--;
            }
            return result;
        }

        
        public void SolvePart2()
        {
            long time = 44806572;
            long distance = 208158110501102;
            long winnable = 0;

            for (long i = 0; i < time; i++)
            {
                long myDistance = i * (time - i);
                if (myDistance > distance) winnable++;
            }
            Console.WriteLine($"Solution Day {day} Part 2: {winnable}");
        }
    }
}