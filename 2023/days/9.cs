using AoC_Day;

namespace AoC2023_Day9
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<Sensor> sensors = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => new Sensor(line)).ToList();
            int result = 0;
            sensors.ForEach(sensor => result += sensor.getPrediction());

            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<Sensor> sensors = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => new Sensor(line)).ToList();
            int result = 0;
            sensors.ForEach(sensor => result += sensor.getPrediction(true));

            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }
    }

    class Sensor{
        List<List<int>> history = [];
        public Sensor (string line){
            history.Add(line.Split(" ").Select(int.Parse).ToList());
            while(expandHistory());
            expandPrediction();
            expandPredictionToLeft();
            Print();
        }

        public int getPrediction(bool fromLeft = false){
            if(fromLeft) return history.First().ToList().First();
            return history.First().ToList().Last();
        }

        private bool expandHistory(){
            List<int> lastSequence = history.Last();
            // Console.WriteLine($"Expanding");
            if(lastSequence.All(s => s == 0)) return false;
            
            List<int> historyOfLastSequence = [];
            for (int i = 0; i < lastSequence.Count -1; i++)
            {
                historyOfLastSequence.Add(lastSequence[i+1] - lastSequence[i]);
                // Console.WriteLine($"adding {lastSequence[i+1] - lastSequence[i]}");
            }
            history.Add(historyOfLastSequence);
            return true;
        }

        public void Print() {
            for (int i = 0; i < history.Count; i++)
            {
                Console.Write(new string(' ', i));
                history[i].ForEach(s => Console.Write(s + " "));
                Console.Write("\n");
            }
        }

        private void expandPrediction(){
            for (int i = history.Count - 1; i >= 0; i--)
            {
                // Console.WriteLine("Expanding Prediction");
                //adding 0 to last sequence
                if(i == history.Count - 1) {
                    history[i].Add(0);
                    continue;
                }
                history[i].Add(history[i].Last() + history[i+1].Last());
            }
        }

        private void expandPredictionToLeft(){
            for (int i = history.Count - 1; i >= 0; i--)
            {
                // Console.WriteLine("Expanding Prediction");
                //adding 0 to last sequence
                if(i == history.Count - 1) {
                    history[i].Add(0);
                    continue;
                }
                history[i].Insert(0, history[i].First() - history[i+1].First());
            }
        }
    }
}