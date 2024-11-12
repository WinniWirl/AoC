using AoC_Day;
using Helper;

namespace AoC2023_Day21
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);

            Queue<Position> currentQueue = [];
            List<Position> newQueue = [];            
            
            //find S
            for (int y = 0; y < input.Count; y++)
            {
                if(input[y].IndexOf('S') != -1) 
                {
                    currentQueue.Enqueue(new Position(input[y].IndexOf('S'), y));
                    break;
                }
            }


            for (int i = 0; i < 64; i++)
            {
                if(i % 100000 == 0) Console.WriteLine(i);
                newQueue = [];
                while(currentQueue.Count > 0) {
                    Position element = currentQueue.Dequeue();

                    if(isDot(element.North(), input)) newQueue.Add(element.North());
                    if(isDot(element.South(), input)) newQueue.Add(element.South());
                    if(isDot(element.West(), input)) newQueue.Add(element.West());
                    if(isDot(element.East(), input)) newQueue.Add(element.East());
                }
                newQueue.Distinct().ToList().ForEach(x => currentQueue.Enqueue(x));
            }

            Console.WriteLine($"Solution Day {day} Part 1: {newQueue.Distinct().Count()}"); //3688 -> +1 because startpoint
        }

        public bool isDot(Position position, List<string> input) {
            if(position.x < 0 || position.x >= input[0].Length) return false;
            if(position.y < 0 || position.y >= input.Count) return false;
            return input[position.y][position.x] == '.';
        }

        public bool isDot2(Position position, List<string> input) {
            int x = 0;
            if (position.x >= 0) x = position.x % input[0].Length;
            else x = input[0].Length + (position.x % input[0].Length); 
            int y = 0;
            if (position.y >= 0) y = position.y % input.Count;
            else y = input.Count + (position.y % input.Count); 
            return input[y][x] == '.';
        }

        public void SolvePart2()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);

            Queue<Position> currentQueue = [];
            List<Position> newQueue = [];            
            
            //find S
            for (int y = 0; y < input.Count; y++)
            {
                if(input[y].IndexOf('S') != -1) 
                {
                    currentQueue.Enqueue(new Position(input[y].IndexOf('S'), y));
                    break;
                }
            }

            int prevResult = 0;
            for (int i = 0; i < 26501365; i++)
            {
                if(i % 100000 == 0) Console.WriteLine(i);
                newQueue = [];
                while(currentQueue.Count > 0) {
                    Position element = currentQueue.Dequeue();

                    if(isDot2(element.North(), input)) newQueue.Add(element.North());
                    if(isDot2(element.South(), input)) newQueue.Add(element.South());
                    if(isDot2(element.West(), input)) newQueue.Add(element.West());
                    if(isDot2(element.East(), input)) newQueue.Add(element.East());
                }
                newQueue.Distinct().ToList().ForEach(x => currentQueue.Enqueue(x));
                Console.WriteLine($"increased by: {newQueue.Distinct().Count() - prevResult}");
                prevResult = newQueue.Distinct().Count();
            }

            Console.WriteLine($"Solution Day {day} Part 1: {newQueue.Distinct().Count()}"); //3688 -> +1 because startpoint
        }
    }
}