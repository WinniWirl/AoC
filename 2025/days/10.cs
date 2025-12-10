using AoC_Day;

namespace AoC2025_Day10
{
    class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var machines = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(input => new Machine(input))
                .PrintEach()
                .ToList();
            
            // Denkhilfe:
            // Ziel: machine.IsAtTarget() == true
            // Aktionen: machine.PressButton(index)
            // Zustand: machine.lights
            // Anfangszustand: alle lights sind aus (false)
            // Es macht keinen sinn einen Button mehrmals zu drücken, da sich der Zustand der Lichter dann wiederholt
            // Auch die reihenfolge der Button presses ist egal, da es nur darauf ankommt ob der button gedrückt wurde oder nicht
            // Daher gibt es maximal 2^n Zustände, wobei n die Anzahl der Buttons
            foreach (var machine in machines)
            {
                var combinations = getAllButtonPressCombinations(machine);
                foreach (var combination in combinations)
                {
                    machine.Reset();
                    foreach (var buttonIndex in combination)
                    {
                        if (machine.minButtonPresses <= combination.Count)
                        {
                            // no need to continue, we already have a better solution
                            break;
                        }
                        var buttonResult = machine.PressButton(buttonIndex);
                        if (buttonResult == 1) // reached target
                        {
                            break;
                        }

                        if (buttonResult == -1) // exceeded min button presses
                        {
                            break;
                        }
                    }
                }
            }
            
            var result = machines.Select(m => m.minButtonPresses).PrintEach("Min Combinations: ").Sum();
            
            Console.WriteLine($"Result: {result}");
        }

        // returns all combinations of button presses (index of pressed buttons)
        public List<List<int>> getAllButtonPressCombinations(Machine machine)
        {
            var combinations = new List<List<int>>();
            var amountOfButtons = machine.buttons.Count;
            var numbers = Enumerable.Range(0, amountOfButtons).ToArray();
            for (var size = 0; size <= amountOfButtons; size++)
            {
                GenerateCombinations(numbers, size, 0, new List<int>(), combinations);
            }
            return combinations;
        }
        
        private static void GenerateCombinations(int[] numbers, int k, int start, List<int> current, List<List<int>> result)
        {
            // Base Case: Found a combination of size k
            if (current.Count == k)
            {
                result.Add(new List<int>(current)); // Add a copy
                return;
            }

            // Recursive Step: Iterate through remaining numbers
            for (int i = start; i < numbers.Length; i++)
            {
                current.Add(numbers[i]); // Include current number
                GenerateCombinations(numbers, k, i + 1, current, result); // Recurse
                current.RemoveAt(current.Count - 1); // Backtrack (remove for next iteration)
            }
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
    
    internal class Button
    {
        public List<int> toggelingLights;
        
        public Button(string input)
        {
            // comes in format (1,2,3,4)
            input = input.Trim('(', ')');
            toggelingLights = input.Split(',').Select(int.Parse).ToList();
        }
        
        public void Press(Machine machine)
        {
            foreach (var lightIndex in toggelingLights)
            {
                machine.lights[lightIndex].Toggle();
            }
        }

        public override string ToString()
        {
            return "B: (" + string.Join(",", toggelingLights) + ")";
        }
    }
    
    internal class Light
    {
        public bool isOn;
        
        public Light()
        {
            isOn = false;
        }
        
        public void Toggle()
        {
            isOn = !isOn;
        }
        
        public override string ToString()
        {
            return isOn ? "#" : ".";
        }
    }
    
    internal class Machine 
    {
        public List<Button> buttons;
        public List<Light> lights;
        public List<Light> target;
        public int buttonPressCount = 0;
        public int minButtonPresses = Int32.MaxValue;
        
        public Machine(string input)
        {
            // input comes in format: [.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}
            // where . is off and # is on for lights, [] is the target () are buttons and {} are not needed yet
            // lights are initially off
            var parts = input.Split(' ');
            var lightStates = parts[0].Trim('[', ']').ToCharArray();
            lights = lightStates.Select(ls => new Light()).ToList();
            target = lightStates.Select(ls => new Light { isOn = ls == '#' }).ToList();
            buttons = parts.Skip(1).TakeWhile(p => p.StartsWith('(')).Select(p => new Button(p)).ToList();
        }
        
        // -1 if exceeds minButtonPresses
        // 0 if not at target yet
        // 1 if at target
        public int PressButton(int buttonIndex)
        {
            buttonPressCount++;
            buttons[buttonIndex].Press(this);
            if (IsAtTarget())
            {
                minButtonPresses = Math.Min(minButtonPresses, buttonPressCount);
                // Console.WriteLine("New min button presses: " + minButtonPresses);
                return 1;
            }
            return buttonPressCount >= minButtonPresses ? -1 : 0;
        }
        
        public void Reset()
        {
            lights.ForEach(light => light.isOn = false);
            buttonPressCount = 0;
        }

        public bool IsAtTarget()
        {
            for (var i = 0; i < lights.Count; i++)
            {
                if (lights[i].isOn != target[i].isOn)
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "Machine(Lights: " + string.Join("", lights) + ", Target: " + string.Join("", target) +
                   ", Buttons: " + string.Join(", ", buttons) + ")";
        }
    }
}