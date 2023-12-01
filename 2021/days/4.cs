using System.Collections;
using System.Globalization;
using System.Numerics;
using AoC_Day;
using Helper;

namespace AoC2021_Day4
{
    class AOCProgram : AoCDay, ISolvable {
      
        public void SolvePart1(){
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            //fist line are nubers to check
            List<int> bingoNumbers = [];
            foreach (string item in input[0].Split(","))
            {
                bingoNumbers.Add(int.Parse(item));
            }

            // create boards
            int line = 2;
            List<Board> boards = [];
            while (input.Count > line){
                int[][] raw_rows = new int[5][];
                for (int i = 0; i < 5; i++)
                {
                    raw_rows[i] = Array.ConvertAll(input[line].Split(" ", StringSplitOptions.RemoveEmptyEntries), item => int.Parse(item.Trim()));
                    line++;
                }
                boards.Add(new Board(raw_rows));
                line++;
            }      

            for (int i = 0; i < bingoNumbers.Count; i++)
            {
                // Console.WriteLine($"Checking Number {bingoNumbers[i]}");
                foreach (Board board in boards)
                {
                    board.checkNumber(bingoNumbers[i]);
                    if (board.checkForBingo()) {
                        Console.WriteLine($"Solution Day {day} Part 1: {board.calcValueOfUncheckedFieldsInBoard() * bingoNumbers[i]}");
                        return;
                    }
                }
            }

        }    

        public void SolvePart2(){
                        List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            //fist line are nubers to check
            List<int> bingoNumbers = [];
            foreach (string item in input[0].Split(","))
            {
                bingoNumbers.Add(int.Parse(item));
            }

            // create boards
            int line = 2;
            List<Board> boards = [];
            while (input.Count > line){
                int[][] raw_rows = new int[5][];
                for (int i = 0; i < 5; i++)
                {
                    raw_rows[i] = Array.ConvertAll(input[line].Split(" ", StringSplitOptions.RemoveEmptyEntries), item => int.Parse(item.Trim()));
                    line++;
                }
                boards.Add(new Board(raw_rows));
                line++;
            }      


            for (int i = 0; i < bingoNumbers.Count; i++)
            {
                // Console.WriteLine($"Checking Number {bingoNumbers[i]}");
                List<Board> notWonBoards = [];

                if(boards.Count == 1)
                {
                    boards[0].checkNumber(bingoNumbers[i]);
                    if (boards[0].checkForBingo()) {
                        Console.WriteLine($"Solution Day {day} Part 2: {boards[0].calcValueOfUncheckedFieldsInBoard() * bingoNumbers[i]}");
                        
                        return;
                    }
                    continue;
                }

                foreach (Board board in boards)
                {
                    board.checkNumber(bingoNumbers[i]);
                    if (board.checkForBingo()) {
                        continue;
                    }
                    notWonBoards.Add(board);
                }
                boards = notWonBoards;
                //Console.WriteLine($"Living boards: {boards.Count}");

            }
        }  
    }

    class Board {
        private (int value, bool check)[][] rows;

        public Board (int[][] rows_raw){
            rows = new (int, bool)[5][];
            rows_raw.ForEachIndexed((row_raw, i) => {
                (int, bool)[] row = new (int, bool)[5];
                row_raw.ForEachIndexed((value, j)=> {
                    row[j] = (value, false);
                });
                rows[i] = row;
            });
        }

        public void checkNumber(int number){
            (int row, int column) position = (-1, -1);
            this.rows.ForEachIndexed ((row, i) =>
            {
                row.ForEachIndexed ((item, j) =>
                {
                    if (item.value == number){
                        position = (j, i);
                        return;
                    }
                });
            });
            if (position.row != -1) {
                rows[position.column][position.row].check = true;
            }
        }

        public int calcValueOfUncheckedFieldsInBoard(){
            int sum = 0;
            this.rows.ForEachIndexed ((row, i) =>
            {
                row.ForEachIndexed ((item, j) =>
                {
                    if (!item.check){
                        sum += item.value;
                    }
                });
            });
            return sum;
        }

        public bool checkForBingo(){
            return checkColumnForBingo(this.rows) || checkRowForBingo(this.rows);
        }

        private bool checkRowForBingo((int, bool)[][] matrix){
            foreach ((int, bool)[] row in matrix)
            {
                bool isBingo = true;
                foreach ((int value, bool ckeck) item in row)
                {
                    if (!item.ckeck)
                    {
                        isBingo = false;
                        break;
                    }
                }
                if(isBingo) return true;
            }
            return false;
        }

        private bool checkColumnForBingo((int, bool)[][] matrix)
        {
            (int, bool)[][] tMatrix = Matrix<(int, bool)>.TransposeMatrix(matrix);
            return checkRowForBingo(tMatrix);
        }
    }
}