using System.Data;
using AoC_Day;
using Helper;

namespace AoC2023_Day16
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            List<char[]> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.ToCharArray()).ToList();
            Tile[,] tiles = new Tile[input.Count, input[0].Length];
            
            input.ForEachIndexed((line, y) => {
                line.ForEachIndexed((item, x) => {
                    tiles[x, y] = new Tile(item, (x, y));
                });
            });

            List<Beam> beams = [new Beam(tiles[3,0], Direction.SOUTH)];
            while(beams.Count > 0) {
                List<Beam> newBeams = [];
                beams.ForEach(beam => newBeams.AddRange(beam.doStep(tiles)));
                beams = newBeams;
            }

            for (int y = 0; y < tiles.GetLength(0); y += 1) {
                for (int x = 0; x < tiles.GetLength(1); x += 1) {
                    Console.Write(tiles[x, y].isEnergized ? "#": ".");
                }
                Console.WriteLine();
            }

            int result = tiles.Cast<Tile>().Where(tile => tile.isEnergized).Count();
            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }

        public void SolvePart2()
        {
            List<char[]> input = Helper.Helper.getInputAsLinesOfCurrentDay(day).Select(line => line.ToCharArray()).ToList();
            Tile[,] tiles = new Tile[input.Count, input[0].Length];
            
            input.ForEachIndexed((line, y) => {
                line.ForEachIndexed((item, x) => {
                    tiles[x, y] = new Tile(item, (x, y));
                });
            });

            int result = 0;

            for (int i = 0; i < tiles.GetLength(0); i++)
            {        
                deenergizeAll(tiles);
                List<Beam> beams = [new Beam(tiles[i,0], Direction.SOUTH)];
                while(beams.Count > 0) {
                    List<Beam> newBeams = [];
                    beams.ForEach(beam => newBeams.AddRange(beam.doStep(tiles)));
                    beams = newBeams;
                }
                result = int.Max(tiles.Cast<Tile>().Where(tile => tile.isEnergized).Count(), result);
            }
            Console.WriteLine("DONE FROM TOP");
            for (int i = 0; i < tiles.GetLength(0); i++)
            {         
                deenergizeAll(tiles);
                List<Beam> beams = [new Beam(tiles[i,tiles.GetLength(1)-1], Direction.NORTH)];
                while(beams.Count > 0) {
                    List<Beam> newBeams = [];
                    beams.ForEach(beam => newBeams.AddRange(beam.doStep(tiles)));
                    beams = newBeams;
                }
                result = int.Max(tiles.Cast<Tile>().Where(tile => tile.isEnergized).Count(), result);
            }
            Console.WriteLine("DONE FROM BOT");
            for (int i = 0; i < tiles.GetLength(1); i++)
            {         
                deenergizeAll(tiles);
                List<Beam> beams = [new Beam(tiles[0,i], Direction.WEST)];
                while(beams.Count > 0) {
                    List<Beam> newBeams = [];
                    beams.ForEach(beam => newBeams.AddRange(beam.doStep(tiles)));
                    beams = newBeams;
                }
                result = int.Max(tiles.Cast<Tile>().Where(tile => tile.isEnergized).Count(), result);
            }
            Console.WriteLine("DONE FROM LEFT");

            for (int i = 0; i < tiles.GetLength(1); i++)
            {         
                deenergizeAll(tiles);
                List<Beam> beams = [new Beam(tiles[tiles.GetLength(1)-1,i], Direction.EAST)];
                while(beams.Count > 0) {
                    List<Beam> newBeams = [];
                    beams.ForEach(beam => newBeams.AddRange(beam.doStep(tiles)));
                    beams = newBeams;
                }
                result = int.Max(tiles.Cast<Tile>().Where(tile => tile.isEnergized).Count(), result);
            }

            Console.WriteLine($"Solution Day {day} Part 1: {result}");
        }
        public void deenergizeAll(Tile[,] tiles){
            foreach (Tile tile in tiles)
            {
                tile.isEnergized = false;
                tile.willResultInLoop = false;
            }
        }
    }

    class Tile{
        public bool isEnergized = false;
        public TileType type;

        public bool willResultInLoop = false;

        public (int x, int y) position;

        public Tile(char tileChar, (int x, int y) position){
            switch (tileChar)
            {
                case '.': { type = TileType.EMPTY; break; }
                case '\\': { type = TileType.LEFT_DOWN_MIRROR; break; }
                case '/': { type = TileType.LEFT_UP_MIRROR; break; }
                case '-': { type = TileType.HORIZONTAL_SPLITTER; break; }
                case '|': { type = TileType.VERTICAL_SPLITTER; break; }
                default: throw new InvalidCastException();
            }
            this.position = position;
        }

        public (int x, int y) positionOfTileIn(Direction direction) {
            switch (direction)
            {
                case Direction.NORTH: return (position.x, position.y - 1);
                case Direction.EAST: return (position.x + 1, position.y);
                case Direction.SOUTH: return (position.x, position.y + 1);
                case Direction.WEST: return (position.x - 1, position.y);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }

    class Beam{
        public Tile? currentTile;
        public Direction beamDirection;

        public Direction[] newBeamDirections {
            get { 
                if(currentTile == null) throw new NoNullAllowedException();
                switch (currentTile.type)
                {
                    case TileType.EMPTY: return [beamDirection];
                    case TileType.HORIZONTAL_SPLITTER: return (int)beamDirection % 2 == 0 ? [Direction.EAST, Direction.WEST] : [beamDirection];
                    case TileType.VERTICAL_SPLITTER: return (int)beamDirection % 2 == 1 ? [Direction.NORTH, Direction.SOUTH] : [beamDirection];
                    case TileType.LEFT_UP_MIRROR: switch (beamDirection) // /
                    {
                        case Direction.NORTH: return [Direction.EAST];
                        case Direction.WEST: return [Direction.SOUTH];
                        case Direction.SOUTH: return [Direction.WEST];
                        case Direction.EAST: return [Direction.NORTH];
                        default: throw new ArgumentOutOfRangeException();
                    }
                    case TileType.LEFT_DOWN_MIRROR: switch (beamDirection) // \
                    {
                        case Direction.NORTH: return [Direction.WEST];
                        case Direction.EAST: return [Direction.SOUTH];
                        case Direction.SOUTH: return [Direction.EAST];
                        case Direction.WEST: return [Direction.NORTH];
                        default: throw new ArgumentOutOfRangeException();
                    }
                    default: throw new InvalidCastException();
                }
            }
        }

        public Beam(Tile currentTile, Direction beamDirection){
            this.currentTile = currentTile;
            currentTile.isEnergized = true;
            this.beamDirection = beamDirection;
        }

        public List<Beam> doStep(Tile[,] tiles)
        {
            Direction[] directions = newBeamDirections;
            List<Beam> beams = [];
            // Console.WriteLine($"x: {currentTile.position.x}, y: {currentTile.position.y}, direction: {beamDirection} -> {directions[0]} & " + (directions.Length==2?directions[1]:"nix"));
            foreach (Direction direction in directions)
            {
                if(currentTile == null) throw new NoNullAllowedException();
                if(currentTile.willResultInLoop) continue;
                (int x, int y) newPosition =  currentTile.positionOfTileIn(direction);
                if(newPosition.x < 0 || newPosition.x >= tiles.GetLength(0) || newPosition.y < 0 || newPosition.y >= tiles.GetLength(1)) continue;
                
                beams.Add(new Beam(tiles[newPosition.x, newPosition.y], direction));
            }
            if(currentTile.type == TileType.HORIZONTAL_SPLITTER || currentTile.type == TileType.VERTICAL_SPLITTER) currentTile.willResultInLoop = true;
            // Console.WriteLine($"{beams.Count}");
            return beams;
        }
    }

    enum TileType{
        EMPTY,
        LEFT_UP_MIRROR,
        LEFT_DOWN_MIRROR,
        HORIZONTAL_SPLITTER,
        VERTICAL_SPLITTER
    }

    enum Direction {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
}