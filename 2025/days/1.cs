using AoC_Day;

namespace AoC2025_Day1
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var inputRaw = Helper.Helper.getInputAsLinesOfCurrentDay(day);

            int position = 50;
            int result = 0;
            foreach (var line in inputRaw)
            {
                char direction = line[0];
                int distance = int.Parse(line.Substring(1));
                if (direction == 'R')
                {
                    position += distance;
                    while (position >= 100)
                    {
                        result++;
                        position -= 100;
                    }
                }
                else if (direction == 'L')
                {
                    position -= distance;
                    while (position < 0)
                    {
                        result++;
                        position += 100;
                    }
                }
                if (position == 0)
                {
                    result++;
                }
            }
            Console.WriteLine(result);
        }

        public void SolvePart2()
        {
            var inputRaw = Helper.Helper.getInputAsLinesOfCurrentDay(day);
        
            int position = 50;
            int result = 0;
            bool wasAtZero = false;
            Console.WriteLine("The dial starts by pointing at: " + position);
            foreach (var line in inputRaw)
            {
                char direction = line[0];
                int distance = int.Parse(line.Substring(1));
                Console.Write("The dial is rotated " + line + " to point at: ");
                if (direction == 'R')
                {
                    position += distance;
                    while (position >= 100)
                    {
                        if (position != 100)
                        {
                            Console.Write("x ");
                            result++;
                        }
                        
                        position -= 100;
                    }
                }
                else if (direction == 'L')
                {
                    position -= distance;
                    while (position < 0)
                    {
                        if (!wasAtZero)
                        {
                            Console.Write("o ");
                            result++;
                        }
                        wasAtZero = false;
                        
                        position += 100;
                    }
                }
                Console.WriteLine(position);
                    wasAtZero = false;
                if (position == 0)
                {
                    Console.WriteLine("boom");
                    result++;
                    wasAtZero = true;
                }
            }
            Console.WriteLine(result);
        }
    }
}