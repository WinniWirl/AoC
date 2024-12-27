using AoC_Day;

namespace AoC2024_Day24
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            throw new NotImplementedException();
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        public List<Wire> prepInput()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var wires = new Dictionary<String, Wire>();
            // var gate = new List<Gate>();
            
            var isWire = true;
            
            foreach (var line in rawInput)
            {
                if (line.Length < 2)
                {
                    isWire = false;
                    continue;
                }

                if (isWire)
                {
                    var split = line.Split(": ");
                    wires.Add(split[0], new Wire(split[1] == "1"));
                }
            }

            throw new NotImplementedException();

        }
    }

    class Gate
    {
        public Wire output = new (null);
        private Wire input1;
        private Wire input2;
        private string opp;

        public bool setInput1(bool input1)
        {
            this.input1 = new Wire(input1);
            return trySetOutput();
        }
        
        public bool setInput2(bool input2)
        {
            this.input2 = new Wire(input2);
            return trySetOutput();
        }

        private bool trySetOutput()
        {
            if (input1 == null || input2 == null) return false;

            switch (opp)
            {
                case "XOR": 
                    output = new(input1.value != input2.value);
                    break;                
                case "OR": 
                    output = new(input1.value!.Value || input2.value!.Value);
                    break;
                case "AND": 
                    output = new(input1.value!.Value && input2.value!.Value);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported operation: {opp}");
            }

            return true;
        }
    }

    record Wire(bool? value);

    // class Wire
    // {
    //     private string name;
    //     private bool? value;
    //
    //     public Wire(string name, bool? value = null)
    //     {
    //         this.name = name;
    //         this.value = value;
    //     }
    //
    //     public Wire(string lineInput)
    //     {
    //         var split = lineInput.Split(": ");
    //         this.name = split[0];
    //         this.value = split[1] == "1";
    //     }
    // }
}