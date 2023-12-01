using System.Collections;
using AdventOfCode;

namespace Helper
{
    static class Helper {

        /**
        *   an indexed foreach.
        */
        public static void ForEachIndexed<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }

        public static List<string> getInputAsLinesOfCurrentDay(string day)
        {
            StreamReader? sr = null;
            List<string> result = [];
            try
            {
                sr = new StreamReader($".\\{AdventOfCode.AdventOfCode.year}\\inputs\\day{day}.txt");
                string? line = sr.ReadLine();
                // Continue to read until you reach end of file
                while (line != null)
                {
                    result.Add(line);
                    line = sr.ReadLine();
                }
            } catch(FileNotFoundException)
            {
                Console.WriteLine($"Input of day {day} not Found!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                sr?.Close();
            }
            
            return result;
        }
    }

    static class Matrix<T>
    {
        public static T[][] TransposeMatrix(T[][] matrix)
        {
            var rows    = matrix.Length;
            var columns = matrix[0].Length;

            var result = new T[columns][];

            for (var c = 0; c < columns; c++)
            {
                result[c] = new T[5];
                for (var r = 0; r < rows; r++)
                {
                    result[c][r] = matrix[r][c];
                }
            }

            return result;
        }
    }
}
