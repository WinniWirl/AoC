using AoC_Day;
using Helper;

namespace AoC2023_Day8
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            char[] commands = input[0].ToCharArray();

            Dictionary<string, (string left, string right)> keyValuePairs = [];
            input.Skip(2).ToList().ForEach(i => {
                string[] tmp = i.Split(" = ");
                string key = tmp[0];
                tmp = tmp[1].Split(", ");
                keyValuePairs.Add(key, (tmp[0].Replace("(", ""), tmp[1].Replace(")", "")));
            });

            string currentKey = "AAA";
            int commandIndex = 0;
            int result = 0;
            while(currentKey != "ZZZ") {
                result++;
                if(commandIndex == commands.Length) commandIndex = 0;
                currentKey = commands[commandIndex] == 'L' ? currentKey = keyValuePairs[currentKey].left : currentKey = keyValuePairs[currentKey].right;
                // Console.WriteLine($"After commandIndex {commandIndex} the currentKey is {currentKey} and result is {result}");
                if(commandIndex % 10000 == 0) Console.WriteLine($"result: {result}");
                commandIndex++;
            }
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
            
        }

        public void SolvePart2()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            char[] commands = input[0].ToCharArray();

            Dictionary<string, (string left, string right)> keyValuePairs = [];
            List<string> startPoints = [];
            List<string> endPoints = [];
            input.Skip(2).ToList().ForEach(i => {
                string[] tmp = i.Split(" = ");
                string key = tmp[0];
                tmp = tmp[1].Split(", ");
                keyValuePairs.Add(key, (tmp[0].Replace("(", ""), tmp[1].Replace(")", "")));
                if(key[2] == 'A') startPoints.Add(key);
                if(key[2] == 'Z') endPoints.Add(key);
            });

            List<(long start, long size)> loops = [];
            startPoints.ForEachIndexed((sp, index) => {
                int loopSize = -1;
                int distanceToLoopStart = -1;
                int counter = 0;
                int commandIndex = 0;
                string currentKey = sp;

                while(loopSize == -1){
                    if(commandIndex == commands.Length) commandIndex = 0;
                    if(endPoints.Where(endPoint => endPoint.Equals(currentKey)).ToArray().Length > 0) {
                        Console.Write(currentKey + " ");
                        if(distanceToLoopStart == -1) {
                            distanceToLoopStart = counter;
                        }
                        else {
                            loopSize = counter - distanceToLoopStart;
                            loops.Add((distanceToLoopStart, loopSize));
                            Console.WriteLine($"Point {sp} has loopSize of {loopSize} and a {distanceToLoopStart} distance to it");
                        }
                    }
                    currentKey = commands[commandIndex] == 'L' ? currentKey = keyValuePairs[currentKey].left : currentKey = keyValuePairs[currentKey].right;
                    commandIndex++;
                    counter++;
                }
            });

            long result = 1;
            loops.ForEach(loop => result = result * loop.size);
            loops.ForEach(loop => result += loop.start);


            // while(loops.Where(l => loops[0].start == l.start).ToArray().Length != loops.Count)
            // {
            //     // long minStart = loops.MinBy(l => l.start).start;
            //     // // Console.WriteLine($"min is {minStart}");
            //     // List<(long start, long size)> tmpLoops = [];
            //     // foreach ((long start, long size) loop in loops)
            //     // {
            //     //     // if(loop.start == minStart) Console.WriteLine($"loop.start = {loop.start} + {loop.size}");
            //     //     tmpLoops.Add((loop.start == minStart ? loop.start + loop.size : loop.start, loop.size));
            //     // }
            //     // loops = tmpLoops;
            // }


            Console.WriteLine($"Solution Day {day} Part 2: {result}");
        }
    }

}