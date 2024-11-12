using AoC_Day;
using Helper;

namespace AoC2023_Day22
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<string> input = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            List<Brick> bricks = [];
            input.ForEach(line => bricks.Add(new Brick(line)));

            
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }

        // private bool doCollide(Brick brick1, Brick brick2)
        // {
        //     // if ()
        // }
    }

    class Brick{
        Position3 from, to;
        public Brick(string s){
            List<List<int>> splitted = s.Split("~").Select(x => x.Split(",").Select(x2 => int.Parse(x2)).ToList()).ToList();
            from = new Position3(splitted[0][0],splitted[0][1],splitted[0][2]);
            to = new Position3(splitted[1][0],splitted[1][1],splitted[1][2]);
        }
        public bool isFloating(List<Brick> bricks){
            foreach (Brick brick in bricks)
            {
                if (brick == this) continue;
                // if (brick.topLayer)
            }
            return false;
        }

        private (Position3 from, Position3 to) topLayer(){
            return (new Position3(from.x, from.y, to.z), to);
        }

        private (Position3 from, Position3 to) groundLayer(){
            return (from, new Position3(to.x, to.y, from.z));
        }

        private (Position3 from, Position3 to) layingOn(){
            return (new Position3(from.x, from.y, from.z-1), new Position3(to.x, to.y, from.z));
        }
    }



 
}