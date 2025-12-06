using AoC_Day;

namespace AoC2025_Day6
{
    class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var values = rawInput.Select(line =>
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToArray()).ToArray();
            var result = 0L;
            for (int valueIndex = 0; valueIndex < values[0].Length; valueIndex++)
            {
                var calcResult = 0L;
                for (int lineIndex = 0; lineIndex < values.Length - 1; lineIndex++)
                {
                    if (values[^1][valueIndex] == "+")
                    {
                        var currentValue = values[lineIndex][valueIndex];
                        Console.WriteLine("CURRENT VALUE: [" + lineIndex + "]" + "[" + valueIndex + "]" + currentValue);
                        var summand = long.Parse(values[lineIndex][valueIndex]);
                        Console.WriteLine("SUMMAND: " + summand + " ");
                        calcResult += summand;
                    }
                    else if (values[^1][valueIndex] == "*")
                    {
                        if (calcResult == 0)
                            calcResult = 1;
                        calcResult *= long.Parse(values[lineIndex][valueIndex]);
                    }
                }

                Console.WriteLine(" = " + calcResult);
                result += calcResult;
            }

            Console.WriteLine("RESULT: " + result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(line => line.ToCharArray())
                .ToArray();
            // search in last row where the operator is
            // operator indicates the start of the values
            // i need to add all values into an array
            var values = new List<GroupedValues>();
            while (rawInput[0].Length > 0)
            {
                var currentValues = new GroupedValues(rawInput[^1][0]);
                // find distance to second operator
                // by disabling the first operator
                rawInput[^1][0] = '.';
                var operatorIndex = new String(rawInput[^1]).IndexOfAny(new char[] { '+', '*' });
                if (operatorIndex == -1)
                {
                    operatorIndex = rawInput[0].Length;
                }

                for (int rowIndex = 0; rowIndex < rawInput.Length; rowIndex++)
                {
                    var value = new String(rawInput[rowIndex], 0, operatorIndex - 1);
                    if (rowIndex < rawInput.Length - 1)
                        currentValues.Values.Add(value);
                    // remove processed chars
                    rawInput[rowIndex] = rawInput[rowIndex][(operatorIndex)..];
                }

                Console.WriteLine(currentValues);
                values.Add(currentValues);
            }

            var result = values.Select(v => v.CalcInSolution2Way()).PrintEach().Sum();
            Console.WriteLine("RESULT: " + result);
        }
    }

    class GroupedValues
    {
        public char Operator;
        public List<string> Values;

        public GroupedValues(char op)
        {
            Operator = op;
            Values = new List<string>();
        }

        public override string ToString()
        {
            return "Operator: " + Operator + ", Values: [" + string.Join(", ", Values) + "]";
        }

        public List<long> ValuesFromTopToBottom()
        {
            var result = new List<long>();
            for (int i = 0; i < Values[0].Length; i++)
            {
                var concatinatedValue = "";
                for (int value = 0; value < Values.Count; value++)
                {
                    var c = this.Values[value][i];
                    if (c != ' ')
                        concatinatedValue += c;
                }

                var longValue = long.Parse(concatinatedValue);
                result.Add(longValue);
            }

            return result;
        }

        public long CalcInSolution2Way()
        {
            var topToBottomValues = ValuesFromTopToBottom();
            var result = 0L;
            if (Operator == '+')
            {
                result += topToBottomValues.Sum();
            }
            else if (Operator == '*')
            {
                result = 1;
                for (int i = 0; i < topToBottomValues.Count; i++)
                {
                    result *= topToBottomValues[i];
                }
            }

            return result;
        }
    }
}