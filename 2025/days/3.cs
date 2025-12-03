using AoC_Day;

namespace AoC2025_Day3
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);

            var result = rawInput.Select(input => GetBiggestNumberInStringOrdered(input))
                .Sum();

            Console.WriteLine(result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);

            var numbers = rawInput
                .Select(input => long.Parse(GetXDigitLongJoltage(input, 12)))
                .ToList();

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine(numbers.Sum());
        }

        // A recursive function
        // find the biggest digit, but not in the last expectedLength digits -1 
        // remove all before it
        // continue until length is expectedLength
        public static string GetXDigitLongJoltage(string input, int expectedLength)
        {
            Console.WriteLine("Processing input: " + input + " (" + input.Length + ")" + " for expected length: " +
                              expectedLength);
            if (input.Length == expectedLength)
            {
                Console.WriteLine("Reached expected length! Need to take the rest: " + input);
                return input;
            }

            if (expectedLength == 0)
            {
                Console.WriteLine("Already reached length! Nothing more to add.");
                return "";
            }

            // find biggest but not in the last of expectedLength
            var saveToSearchChars = input.Substring(0, input.Length - expectedLength + 1);
            var charsToFindBiggest = saveToSearchChars.ToCharArray();
            Array.Sort(charsToFindBiggest);
            Array.Reverse(charsToFindBiggest);
            var biggestChar = charsToFindBiggest[0];
            var indexOfBiggest = saveToSearchChars.IndexOf(biggestChar);
            Console.WriteLine("biggest char to keep: " + biggestChar + " at index " + indexOfBiggest);
            // remove it and all before
            var newInput = input.Substring(indexOfBiggest + 1);
            return biggestChar + GetXDigitLongJoltage(newInput, expectedLength - 1);
        }

        public static long GetBiggestXDigitNumberInStringV2(string input, int expectedLength, int lockedIndex)
        {
            Console.WriteLine("Processing input: " + input);
            //find largest but not in the last of expectedLength
            var saveToSearchChars = input.Substring(lockedIndex, expectedLength);
            var charsToFindBiggest = saveToSearchChars.ToCharArray();
            Array.Sort(charsToFindBiggest);
            Array.Reverse(charsToFindBiggest);
            var biggestChar = charsToFindBiggest[0];
            var indexOfBiggest = saveToSearchChars.IndexOf(biggestChar);
            Console.WriteLine("biggest char to keep: " + biggestChar + " at index " + indexOfBiggest);
            //remove all before it
            input = input.Substring(indexOfBiggest);
            Console.WriteLine("After removing before biggest: " + input);
            //continue with that process until length is expectedLength -> recursive
            if (input.Length == expectedLength)
            {
                Console.WriteLine(input);
                return long.Parse(input);
            }
            else
            {
                return GetBiggestXDigitNumberInStringV2(input, expectedLength - lockedIndex, lockedIndex + 1);
            }
        }

        public static long GetBiggestXDigitNumberInString(string input, int expectedLength)
        {
            Console.WriteLine("Processing input: " + input);
            while (input.Length > expectedLength)
            {
                var chars = input.ToCharArray();

                // get smalest 
                Array.Sort(chars);
                var smallestChar = chars[0];
                var indexToRemove = input.LastIndexOf(smallestChar);
                Console.WriteLine("smallest char to remove: " + smallestChar + " at index " + indexToRemove);
                // remove it
                input = input.Remove(indexToRemove, 1);
            }

            Console.WriteLine(input);
            return long.Parse(input);
        }

        public static int GetBiggestNumberInStringOrdered(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);
            Array.Reverse(chars);
            var big1 = chars[0];
            var big2 = chars[1];

            if (big1 == big2)
            {
                var result = int.Parse(big1.ToString() + big2.ToString());
                Console.WriteLine(result);
                return result;
            }

            if (input.IndexOf(big1) < input.IndexOf(big2))
            {
                var result = int.Parse(big1.ToString() + big2.ToString());
                Console.WriteLine(result);
                return result;
            }

            if (input.IndexOf(big1) == chars.Length - 1)
            {
                var result = int.Parse(big2.ToString() + big1.ToString());
                Console.WriteLine(result);
                return result;
            }

            var bigIndedx = input.IndexOf(big1);
            var substr = input.Substring(bigIndedx + 1);
            chars = substr.ToCharArray();
            Array.Sort(chars);
            Array.Reverse(chars);
            var nextBig = chars[0];
            var fresult = int.Parse(big1.ToString() + nextBig.ToString());
            Console.WriteLine(fresult);
            return fresult;
        }

        public static int GetBiggestNumbersInString(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);
            Array.Reverse(chars);
            var big1 = chars[0];
            var big2 = chars[1];

            //search to get correct order in original string
            var result = 0;
            if (big1 == big2)
            {
                result = int.Parse(big1.ToString() + big2.ToString());
                Console.WriteLine(result);
                return result;
            }

            var index1 = input.IndexOf(big1);
            var index2 = input.IndexOf(big2);

            if (index1 < index2)
            {
                result = int.Parse(big1.ToString() + big2.ToString());
                Console.WriteLine(result);
                return result;
            }

            result = int.Parse(big2.ToString() + big1.ToString());
            Console.WriteLine(result);
            return result;
        }
    }
}