using AoC_Day;

namespace AoC2023_Day5
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<long> input = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0].Split(" ").Skip(1).Select(long.Parse).ToList();
            List<Seed> seeds = [];
            for (int i = 0; i < input.Count; i = i+2)
            {
                for (int j = 0; j < input[i+1]; j++)
                {
                    seeds.Add(new Seed(input[i]+j));           
                    // Console.WriteLine($"add seed {i+j}");    
                    if(seeds.Count % 100000 == 0) Console.WriteLine($"Added seeds {seeds.Count}");
                }
            }

            foreach (Seed seed in seeds)
            {
                bool needsNewCategory = false;                
                foreach (string line in Helper.Helper.getInputAsLinesOfCurrentDay(day).Skip(2))
                {
                    if(line.Length == 0)
                    {
                        needsNewCategory = false;
                        continue;
                    }

                    if(!needsNewCategory && int.TryParse(line[0].ToString(), out _))
                    {
                        // Console.Write($"{seed.seed} to ");
                        List<long> values = line.Split(" ").Select(long.Parse).ToList();
                        long toSource = seed.seed - values[1];
                        if(toSource >= 0 && toSource < values[2])
                        {
                            seed.seed = values[0] + toSource;
                            needsNewCategory = true;    
                        }
                        // Console.WriteLine($"{seed.seed}");   
                    }
                }
                // Console.WriteLine($"{seed.seed}");       
            }

            long result = seeds.Min(s => s.seed);
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
        }

        class Seed{
            public long seed;
            public Seed(long seed) {
                this.seed = seed;
            }
        }
    }
}