using AoC_Day;

namespace AoC2025_Day8
{
    internal class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            const int numberOfConnectionsToProcess = 1000;

            var result = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(input => new JunctionBox(input))
                .ToList()
                .Pipe(GetAllPossibleConnections)
                .OrderBy(c => c.Length())
                .Take(numberOfConnectionsToProcess)
                .Aggregate(
                    new List<ConnectionGroup>(),
                    (groups, connection) => ProcessConnectionGroup(connection, groups)
                )
                .OrderByDescending(g => g.CountBoxes())
                .Take(3)
                .Aggregate(1, (acc, group) => acc * group.CountBoxes());

            Console.WriteLine($"Result: {result}");
        }

        public void SolvePart2()
        {
            var junctionBoxes = Helper.Helper.getInputAsLinesOfCurrentDay(day)
                .Select(input => new JunctionBox(input))
                .ToList();

            var sortedConnections = GetAllPossibleConnections(junctionBoxes)
                .OrderBy(c => c.Length())
                .ToList();

            var connectionGroups = new List<ConnectionGroup>();
            var i = 0;

            do
            {
                Console.WriteLine("Connecting: " + sortedConnections[i] + " at index " + i);
                connectionGroups = ProcessConnectionGroup(sortedConnections[i], connectionGroups);

                i++;
            } while (connectionGroups.Count < 1 || connectionGroups[0].CountBoxes() < junctionBoxes.Count);

            var lastConnection = sortedConnections[i - 1];
            Console.WriteLine($"Last Connection used: {lastConnection} at index {i - 1}");

            var result = lastConnection.Box1.x * lastConnection.Box2.x;
            Console.WriteLine($"Result: {result}");
        }

        /// <summary>
        /// Processes a connection and assigns it to existing groups or creates a new group.
        /// If the connection belongs to multiple groups, it merges them.
        /// </summary>
        /// <param name="connection">The connection to process</param>
        /// <param name="connectionGroups">The list of existing connection groups</param>
        private static List<ConnectionGroup> ProcessConnectionGroup(Connection connection,
            List<ConnectionGroup> connectionGroups)
        {
            var matchingGroups = IsPartOfExistingConnectionGroup(connection, connectionGroups);
            switch (matchingGroups.Count)
            {
                case 0:
                {
                    var newGroup = new ConnectionGroup([connection]);
                    connectionGroups.Add(newGroup);
                    break;
                }
                case 1:
                    matchingGroups[0].AddConnection(connection);
                    break;
                default:
                {
                    var newGroup = new ConnectionGroup([connection]);
                    foreach (var group in matchingGroups)
                    {
                        newGroup.AddConnections(group);
                        connectionGroups.Remove(group);
                    }

                    connectionGroups.Add(newGroup);
                    break;
                }
            }

            return connectionGroups;
        }

        /// <summary>
        /// Finds all connection groups that contain at least one box from the given connection.
        /// </summary>
        private static List<ConnectionGroup> IsPartOfExistingConnectionGroup(Connection connection,
            List<ConnectionGroup> groups) =>
            groups.Where(group => group.IsPartOfConnection(connection)).ToList();

        /// <summary>
        /// Generates all possible connections between junction boxes (n choose 2).
        /// </summary>
        private static List<Connection> GetAllPossibleConnections(List<JunctionBox> boxes) =>
            boxes.SelectMany((box, i) => boxes
                    .Skip(i + 1)
                    .Select(otherBox => new Connection(box, otherBox)))
                .ToList();
    }

    internal class ConnectionGroup(List<Connection> connections)
    {
        private readonly List<Connection> _connections = connections;

        public void AddConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public void AddConnections(List<Connection> otherConnections)
        {
            this._connections.AddRange(otherConnections);
        }

        public void AddConnections(ConnectionGroup group)
        {
            this._connections.AddRange(group._connections);
        }

        public bool IsPartOfConnection(Connection connection) =>
            _connections.Any(t => t.HasOverlap(connection));

        public int CountBoxes() =>
            _connections
                .SelectMany(c => new[] { c.Box1, c.Box2 })
                .ToHashSet()
                .Count;

        public override string ToString() =>
            $"ConnectionGroup({string.Join(", ", _connections)})";
    }

    internal class Connection(JunctionBox box1, JunctionBox box2)
    {
        public readonly JunctionBox Box1 = box1, Box2 = box2;

        public double Length() => Box1.DistanceTo(Box2);

        private bool HasBox(JunctionBox obj) =>
            Box1.Equals(obj) || Box2.Equals(obj);

        public bool HasOverlap(Connection other) =>
            HasBox(other.Box1) || HasBox(other.Box2);

        public override string ToString() =>
            $"Connection({Box1} <-> {Box2})";
    }

    internal class JunctionBox
    {
        public readonly long x, y, z;

        public JunctionBox(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public JunctionBox(string junction)
        {
            var split = junction.Split(',');
            this.x = long.Parse(split[0]);
            this.y = long.Parse(split[1]);
            this.z = long.Parse(split[2]);
        }

        public override string ToString() =>
            $"JunctionBox({x},{y},{z})";

        public double DistanceTo(JunctionBox other)
        {
            var (dx, dy, dz) = (x - other.x, y - other.y, z - other.z);
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override bool Equals(object? obj) =>
            obj is JunctionBox other && x == other.x && y == other.y && z == other.z;

        public override int GetHashCode() =>
            HashCode.Combine(x, y, z);
    }
}