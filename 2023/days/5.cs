using AoC_Day;

namespace AoC2023_Day5
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<Seed> seeds = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0].Split(" ").Skip(1).Select(s => new Seed(long.Parse(s))).ToList();

            Console.WriteLine($"Added seeds {seeds.Count}");
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
                        List<long> values = line.Split(" ").Select(long.Parse).ToList();
                        long toSource = seed.seed - values[1];
                        if(toSource >= 0 && toSource < values[2])
                        {
                            seed.seed = values[0] + toSource;
                            needsNewCategory = true;    
                        }
                    }
                }
            }

            long result = seeds.Min(s => s.seed);
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<long[]> firstLineSplitted = Helper.Helper.getInputAsLinesOfCurrentDay(day)[0].Split(" ").Skip(1).Select(long.Parse).Chunk(2).ToList();
            List<SeedRange> seeds = firstLineSplitted.Select(x => new SeedRange(x[0], x[0] + x[1]-1)).ToList();

            //categorys
            List<List<string>> categories = Helper.Helper.getInputAsGroupOfCurrentDay(day).Skip(1).ToList();
            Console.WriteLine("categories " + categories.Count);

            for (long testSeed = 0; testSeed < long.MaxValue; testSeed++)
            {
                long result = testSeed;
                if(testSeed % 100000 == 0) 
                    Console.WriteLine($"Testing seed {testSeed}");
                for (int c = categories.Count - 1; c >= 0; c--)
                {
                    bool isChangedInThisCategory = false;
                    foreach (string line in categories[c])
                    {
                        if(!isChangedInThisCategory)
                        {
                            List<long> values = line.Split(" ").Select(long.Parse).ToList();
                            (long destination, long source, long range) pattern = (values[0], values[1], values[2]);

                            long overlap = result - pattern.destination;
                            if(overlap >= 0 && overlap < pattern.range)
                            {
                                result = pattern.source + overlap;
                                isChangedInThisCategory = true;    
                            }
                        }
                    }
                }
                foreach(SeedRange seed in seeds) {
                    if (seed.isInRange(result)) {
                        Console.WriteLine($"Solution Day {day} Part 2: {testSeed}");
                        return;
                    }
                }   
            }

        }

        class Seed{
            public long seed;
            public Seed(long seed) {
                this.seed = seed;
            }
        }

        class SeedRange{
            public long from;
            public long to;

            // public long size {
            //     get{};
            // };

            public SeedRange(long from, long to)
            {
                this.from = from;
                this.to = to;
            }

            public bool isInRange(long num) {
                return from <= num && to >= num;
            }
            // public List<SeedRange> Split(long position){
            //     List<SeedRange> result = [];
            //     if(this.from < position && this.to >= position ){
            //         result.Add(new SeedRange(from, position));
            //         result.Add(new SeedRange(position, to));
            //     }
            //     return result;
            // }
        }
    }
}