using AoC_Day;

namespace AoC2025_Day8
{
    internal class AocProgram : AoCDay, ISolvable
    {
            public void SolvePart1()
        {
            const int numberOfConnectionsToProcess = 1000;
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var junctionBoxes = rawInput.Select(input => new JunctionBox(input)).ToList();
            var connections = GetAllPossibleConnections(junctionBoxes);
            connections = connections.OrderBy(c => c.Length()).ToList();
            
            var connectionGroups = new List<ConnectionGroup>();
            
            for (var i = 0; i < numberOfConnectionsToProcess; i++)
            {
                var connection = connections[i];

                ProcessConnectionGroup(connection, connectionGroups);
            }

            //take 3 largest connection groups
            Console.WriteLine("Total Amount Connections: " + connections.Count);
            Console.WriteLine("Total Amount found connection Groups: " + connectionGroups.Count);
            connectionGroups = connectionGroups.OrderByDescending(g => g.CountBoxes()).Take(3).ToList();
            var result = connectionGroups.Aggregate(1, (acc, group) => acc * group.CountBoxes());
            Console.WriteLine("Result: " + result);
        }

        public void SolvePart2()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var junctionBoxes = rawInput.Select(input => new JunctionBox(input))
                // .PrintEach()
                .ToList();
            var connections = GetAllPossibleConnections(junctionBoxes);
            connections = connections.OrderBy(c => c.Length()).ToList();

            var connectionGroups = new List<ConnectionGroup>();

            var connection = connections[0];
            var i = 0;

            while (connectionGroups.Count > 1 || connectionGroups[0].CountBoxes() < junctionBoxes.Count)
            {
                connection = connections[i];
                Console.WriteLine("Connecting: " + connection + " at index " + i);

                var matchingGroups = IsPartOfExistingConnectionGroup(connection, connectionGroups);

                switch (matchingGroups.Count)
                {
                    case 0:
                    {
                        // Console.WriteLine("No matching connection groups found");
                        var newGroup = new ConnectionGroup([connection]);
                        connectionGroups.Add(newGroup);
                        break;
                    }
                    case 1:
                        // Console.WriteLine("Matching connection group found");
                        matchingGroups[0].AddConnection(connection);
                        break;
                    default:
                    {
                        // Console.WriteLine("Multiple matching connection groups found, merging them");
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

                i++;
            }
            
            Console.WriteLine("Total Amount Connections: " + connections.Count);
            Console.WriteLine("Total Amount found connection Groups: " + connectionGroups.Count);
            Console.WriteLine("Last Connection used: " + connection + " at index " + i);

            var result = connection.Box1.x * connection.Box2.x;
            Console.WriteLine("Result: " + result);
        }

        private static List<ConnectionGroup> IsPartOfExistingConnectionGroup(Connection connection,
            List<ConnectionGroup> groups)
        {
            return groups.Where(group => group.IsPartOfConnection(connection)).ToList();
        }

        private static List<Connection> GetAllPossibleConnections(List<JunctionBox> boxes)
        {
            var connections = new List<Connection>();
            for (var i = 0; i < boxes.Count; i++)
            {
                for (var j = i + 1; j < boxes.Count; j++)
                {
                    connections.Add(new Connection(boxes[i], boxes[j]));
                }
            }

            return connections;
        }

        private static void ProcessConnectionGroup(Connection connection, List<ConnectionGroup> connectionGroups)
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
        }
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

        public bool IsPartOfConnection(Connection connection)
        {
            return _connections.Any(t => t.HasOverlap(connection));
        }
        
        public int CountBoxes()
        {
            var boxes = new HashSet<JunctionBox>();
            foreach (var connection in _connections)
            {
                boxes.Add(connection.Box1);
                boxes.Add(connection.Box2);
            }

            return boxes.Count;
        }
        
        public override string ToString()
        {
            return "ConnectionGroup(" + string.Join(", ", _connections.Select(c => c.ToString())) + ")";
        }
    }

    internal class Connection(JunctionBox box1, JunctionBox box2)
    {
        public readonly JunctionBox Box1 = box1, Box2 = box2;

        public double Length()
        {
            return Box1.DistanceTo(Box2);
        }

        private bool HasBox(JunctionBox obj)
        {
            return this.Box1.Equals(obj) || this.Box2.Equals(obj);
        }

        public bool HasOverlap(Connection other)
        {
            return this.HasBox(other.Box1) || this.HasBox(other.Box2);
        }

        public override string ToString()
        {
            return "Connection(" + Box1.ToString() + " <-> " + Box2.ToString() + ")";
        }
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

        public override string ToString()
        {
            return "JunctionBox(" + x + "," + y + "," + z + ")";
        }

        public double DistanceTo(JunctionBox other)
        {
            var dx = this.x - other.x;
            var dy = this.y - other.y;
            var dz = this.z - other.z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override bool Equals(object? obj)
        {
            if (obj is JunctionBox other)
            {
                return this.x == other.x && this.y == other.y && this.z == other.z;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode();
        }
    }
}