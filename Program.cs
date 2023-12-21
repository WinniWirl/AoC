using System.Net;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;

    namespace AdventOfCode {
    class AdventOfCode{
        
        const string sessionId = "53616c7465645f5f110b5769983c17a48573443c034e35f485be62c121103663f626d5fd36b4ceff53b63e01480f895e9c2e1adda801477ca2591ecad458f40f";
        public static string year = "2023";
        public static async Task Main(String[] args) {
            string currentDay = DateTime.Today.Day.ToString();
            Console.WriteLine($"Ho ho ho ho! Frohen {currentDay}. Dezember!");

            //FAKE DAY
            currentDay = "13";
            await CheckForInputOfDay(currentDay);
            runDay(currentDay);
        }    

        private static void runDay(string day){
            Type? classOfDay = Type.GetType($"AoC{year}_Day{day}.AOCProgram");
            if (classOfDay == null) {
                Console.WriteLine($"Für Tag {day} ist kein namespace gefunden worden.");
                return;
            }
            dynamic? program = Activator.CreateInstance(classOfDay);
            program!.day = day;

            Console.WriteLine($"Part 1 oder Part 2?");
            string? userInputSolve = Console.ReadLine();
            if ( userInputSolve == "1") { program.SolvePart1(); }
            else if ( userInputSolve == "2") { program.SolvePart2(); }
            // if (result != null){
            //     Console.WriteLine($"Result is: {result}");
            //     Console.WriteLine($"Do you like to post the result? [Y|n]");
            //     if(Console.ReadLine().ToLower() != "n") return;

            // }
        }

        private static async Task CheckForInputOfDay(string day)
        {
            bool found = true;
            StreamReader? sr = null;
            string? line;
            try
            {
                sr = new StreamReader($".\\{year}\\inputs\\day{day}.txt");
                line = sr.ReadLine();
                if (line == null)
                {
                    Console.WriteLine($"Input of day {day} is empty!");
                    found = false;
                }
                //Continue to read until you reach end of file
                // while (line != null)
                // {
                //     Console.WriteLine(line);
                //     line = sr.ReadLine();
                // }
            }
            catch(FileNotFoundException)
            {
                found = false;
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

            if (!found) {
                await downloadInputOfDay(day);
            }
        }

        private static async Task downloadInputOfDay(string day){
            StreamWriter? sw = null;
            try{
                sw = new StreamWriter($".\\{year}\\inputs\\day{day}.txt");
                
                Console.WriteLine("Start downloading Input");            
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cookie", $"session={sessionId}");
                HttpResponseMessage response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
                string responseBody = await response.Content.ReadAsStringAsync();
                await sw.WriteAsync(responseBody);
                Console.WriteLine($"Downloaded input of day {day} in {year} successfully!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                sw?.Close();
            }
        }
    }
}