using AoC_Day;

namespace AoC2024_Day1
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var inputRaw = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var input = inputRaw.Select(x => x.Split("   ").Select(int.Parse).ToArray()).ToArray();

            var list1 = input.Select(x => x[0]).OrderBy(x => x).ToArray();
            var list2 = input.Select(x => x[1]).OrderBy(x => x).ToArray();

            var sum = 0;

            for (var i = 0; i < list1.Length; i++)
            {
                sum += Math.Abs(list1[i] - list2[i]);
            }

            Console.WriteLine(sum);
        }

        public void SolvePart2()
        {
            var inputRaw = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var input = inputRaw.Select(x => x.Split("   ").Select(int.Parse).ToArray()).ToArray();

            var list1 = input.Select(x => x[0]).ToArray();
            var list2 = input.Select(x => x[1]).ToArray();

            var frequencyMap1 = ToFrequencyMap(list1);
            var frequencyMap2 = ToFrequencyMap(list2);

            var sum = 0;
            foreach (var kv in frequencyMap1)
            {
                if (frequencyMap2.TryGetValue(kv.Key, out var value))
                    sum += kv.Value * kv.Key * value ;
            }
            Console.WriteLine(sum);
        }

        private static Dictionary<int, int> ToFrequencyMap(int[] list) =>
            list.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    }
}