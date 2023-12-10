using AoC_Day;

namespace AoC2023_Day10
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<List<char>> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(x => x.ToCharArray().ToList()).ToList();
            //Finding S
            int rowOfS = lines.IndexOf(lines.First(line => line.Contains('S')));
            int indexOfS = lines[rowOfS].IndexOf('S');
            //cheating here! (looked in input)
            //Start
            Pipe currentPipe = new Pipe((rowOfS, indexOfS), '-', Direction.WEST,  true);
            
            int result = 0;
            do
            {
                result++;
                (int row, int index) newPosition = currentPipe.newPosition;
                currentPipe = new Pipe(newPosition, lines[newPosition.row][newPosition.index], opositeOf(currentPipe.goingTo));   
                // Console.WriteLine($"At {currentPipe.position.row},{currentPipe.position.index} is pipe of type {currentPipe.pipeType}");        
            } while (!currentPipe.isStart);

            Console.WriteLine($"Solution Day {day} Part 1: {result/2}");
        }

        public void SolvePart2()
        {
            List<List<char>> lines = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(x => x.ToCharArray().ToList()).ToList();
            //Finding S
            int rowOfS = lines.IndexOf(lines.First(line => line.Contains('S')));
            int indexOfS = lines[rowOfS].IndexOf('S');
            //cheating here! (looked in input)
            //Start
            Pipe currentPipe = new Pipe((rowOfS, indexOfS),  '-', Direction.WEST, true);
            
            List<Pipe> loop = [];
            do
            {
                loop.Add(currentPipe);
                (int row, int index) newPosition = currentPipe.newPosition;
                currentPipe = new Pipe(newPosition, lines[newPosition.row][newPosition.index], opositeOf(currentPipe.goingTo));   
            } while (!currentPipe.isStart);

            List<Pipe> rightPipes = [];
            List<Pipe> leftPipes = [];
            foreach (Pipe inLoopPipe in loop)
            {
                //leftPipes
                foreach (Direction direction in inLoopPipe.relativeLeft())
                {
                    (int row, int index) newPosition = inLoopPipe.getPositionOfPipeIn(direction);
                    if(!inBoundings(newPosition, lines)) continue;
                    Pipe dummy = new Pipe(newPosition, lines[newPosition.row][newPosition.index]);
                    while(!loop.Contains(dummy) && inBoundings(dummy.position, lines)){
                        leftPipes.Add(dummy);
                        dummy = new Pipe(dummy.getPositionOfPipeIn(direction));
                    }
                }
                //rightPipes
                foreach (Direction direction in inLoopPipe.relativeLeft())
                {
                    (int row, int index) newPosition = inLoopPipe.getPositionOfPipeIn(opositeOf(direction));
                    if(!inBoundings(newPosition, lines)) continue;
                    Pipe dummy = new Pipe(newPosition, lines[newPosition.row][newPosition.index]);
                    while(!loop.Contains(dummy)){
                        rightPipes.Add(dummy);
                        dummy = new Pipe(dummy.getPositionOfPipeIn(opositeOf(direction)));
                    }
                }
            }

            //printing Maze
            for (int x = 0; x < lines.Count; x++)
            {
                for (int y = 0; y < lines[0].Count; y++)
                {
                    Pipe dummy = new Pipe((x, y));
                    // if (loop.Contains(dummy)) Console.Write(loop[loop.IndexOf(dummy)].pipeType);
                    if (loop.Contains(dummy)) Console.Write("*");
                    else if (rightPipes.Contains(dummy)) Console.Write("I");
                    else if (leftPipes.Contains(dummy)) Console.Write("O");
                    else Console.Write(" ");
                }
                Console.Write("\n");
            }

            Console.WriteLine($"Solution Day {day} Part 2: {rightPipes.Distinct().Count()} (right -> I) or {leftPipes.Distinct().Count()} (left -> O)");
        }

        public Direction opositeOf(Direction direction)
        {
            switch (direction)
            {
                case Direction.NORTH: return Direction.SOUTH;
                case Direction.EAST: return Direction.WEST;
                case Direction.SOUTH: return Direction.NORTH;
                case Direction.WEST: return Direction.EAST;
                default: return Direction.NORTH;
            }
        }

        public bool inBoundings((int row, int index) position, List<List<char>> input){
            if(position.row < 0 || position.row >= input.Count) return false;
            if(position.index < 0 || position.index >= input[0].Count) return false;
            return true;
        }
    }

    

    class Pipe {
        public (int row, int index) position;
        public Direction commingFrom;

        public char pipeType;

        public bool isStart;

        public Pipe((int row, int index) position, char pipeType = '*', Direction commingFrom = Direction.NORTH, bool isStart = false){
            this.position = position;
            this.commingFrom = commingFrom;
            this.pipeType = pipeType;
            this.isStart = pipeType == 'S';
        }

        public Direction goingTo {
            get { 
                switch (pipeType)
                {
                    case '-': return commingFrom == Direction.WEST ? Direction.EAST : Direction.WEST;
                    case '|': return commingFrom == Direction.NORTH ? Direction.SOUTH : Direction.NORTH;
                    case 'L': return commingFrom == Direction.NORTH ? Direction.EAST : Direction.NORTH;
                    case 'J': return commingFrom == Direction.NORTH ? Direction.WEST : Direction.NORTH;
                    case '7': return commingFrom == Direction.SOUTH ? Direction.WEST : Direction.SOUTH;
                    case 'F': return commingFrom == Direction.SOUTH ? Direction.EAST : Direction.SOUTH;
                    default: return Direction.NORTH;
                }
            }
        }

        public (int row, int index) getPositionOfPipeIn(Direction direction){
            switch (direction)
            {
                case Direction.NORTH: return (position.row - 1, position.index);
                case Direction.SOUTH: return (position.row + 1, position.index);
                case Direction.WEST: return (position.row, position.index - 1);
                case Direction.EAST: return (position.row, position.index + 1);
                default: return position;
            }
        }


        
        public (int row, int index) newPosition {
            get {
                return getPositionOfPipeIn(goingTo);
            }
        }

        public List<Direction> relativeLeft()
        {
            List<Direction> directions = [];
            directions.Add((Direction)(((int)commingFrom + 1)%4));
            if (this.pipeType == '|' || this.pipeType == '-') return directions;
            directions.Add((Direction)(((int)goingTo + 3)%4)); // + 4 - 1 = +3
            return directions;
        }
        
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Pipe other = (Pipe)obj;
            if (position.row != other.position.row || position.index != other.position.index) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 3;
                hash = hash * 23 * position.row.GetHashCode();
                hash = hash * 23 * position.index.GetHashCode();
                return hash;
            }
        }
    }

    enum Direction {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
}