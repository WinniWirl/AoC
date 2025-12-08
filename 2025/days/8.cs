using AoC_Day;

namespace AoC2025_Day8
{
    class AocProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            var rawInput = Helper.Helper.getInputAsLinesOfCurrentDay(day);
            var junctionBoxes = rawInput.Select(input => new JunctionBox(input))
                // .PrintEach()
                .ToList();
            var connections = GetAllPossibleConnections(junctionBoxes);
            connections = connections.OrderBy(c => c.Length()).ToList();
            
            var connectionGroups = new List<ConnectionGroup>();

            for (int i = 0; i < 1000; i++)
            {
                var connection = connections[i];
                
                var matchingGroups = isPartOfExistingConnectionGroup(connection, connectionGroups);
                
                if (matchingGroups.Count == 0)
                {
                    // Console.WriteLine("No matching connection groups found");
                    var newGroup = new ConnectionGroup(new List<Connection> { connection });
                    connectionGroups.Add(newGroup);
                }
                else if (matchingGroups.Count == 1)
                {
                    // Console.WriteLine("Matching connection group found");
                    matchingGroups[0].AddConnection(connection);
                }
                else
                {
                    // Console.WriteLine("Multiple matching connection groups found, merging them");
                    var newGroup = new ConnectionGroup(new List<Connection> { connection });
                    foreach (var group in matchingGroups)
                    {
                        newGroup.AddConnections(group);
                        connectionGroups.Remove(group);
                    }

                    connectionGroups.Add(newGroup);
                }
            }
            
            Console.WriteLine("Total Amount Connections: " + connections.Count);
            Console.WriteLine("Total Amount found connection Groups: " + connectionGroups.Count);
            // Console.WriteLine(connectionGroups[0]);
            // result is product of sizes of each connection group
            // foreach (var connectionGroup in connectionGroups)
            // {
            //     Console.WriteLine("COUNT: " + connectionGroup.CountBoxes());
            //     // Console.WriteLine(connectionGroup);
            // }
            
            //take 3 largest connection groups
            connectionGroups = connectionGroups.OrderByDescending(g => g.CountBoxes()).Take(3).ToList();
            var result = connectionGroups.Aggregate(1, (acc, group) => acc * group.CountBoxes());
            Console.WriteLine("Result: " + result);
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
        
        public List<ConnectionGroup> isPartOfExistingConnectionGroup(Connection connection, List<ConnectionGroup> groups)
        {
            var matchingGroups = new List<ConnectionGroup>();
            foreach (var group in groups)
            {
                if (group.IsPartOfConnection(connection))
                {
                    matchingGroups.Add(group);
                }
            }
            return matchingGroups;
        }
        
        public List<Connection> GetAllPossibleConnections(List<JunctionBox> boxes)
        {
            var connections = new List<Connection>();
            for (int i = 0; i < boxes.Count; i++)
            {
                for (int j = i + 1; j < boxes.Count; j++)
                {
                    connections.Add(new Connection(boxes[i], boxes[j]));
                }
            }
            return connections;
        }
    }

    class ConnectionGroup(List<Connection> connections)
    {
        public readonly List<Connection> Connections = connections;

        public void AddConnection(Connection connection)
        {
            Connections.Add(connection);
        }

        public void AddConnections(List<Connection> otherConnections)
        {
            this.Connections.AddRange(otherConnections);
        }
        
        public void AddConnections(ConnectionGroup group)
        {
            this.Connections.AddRange(group.Connections);
        }

        public bool IsPartOfConnection(Connection connection)
        {
            return Connections.Any(t => t.HasOverlap(connection));
        }
        
        public override string ToString()
        {
            return "ConnectionGroup(" + string.Join(", ", Connections.Select(c => c.ToString())) + ")";
        }
        
        public int CountBoxes()
        {
            var boxes = new HashSet<JunctionBox>();
            foreach (var connection in Connections)
            {
                boxes.Add(connection.Box1);
                boxes.Add(connection.Box2);
            }
            return boxes.Count;
        }
    }
    
    class Connection(JunctionBox box1, JunctionBox box2)
    {
        public readonly JunctionBox Box1 = box1, Box2 = box2;

        public double Length()
        {
            return Box1.DistanceTo(Box2);
        }

        public bool HasBox(JunctionBox obj)
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

    class JunctionBox
    {
        private readonly long x, y, z;

        public JunctionBox(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public JunctionBox(string junction)
        {
            string[] split = junction.Split(',');
            this.x = long.Parse(split[0]);
            this.y = long.Parse(split[1]);
            this.z = long.Parse(split[2]);
        }

        public override string ToString()
        {
            return "JunctionBox(" + x + ", " + y + ", " + z + ")";
        }

        public double DistanceTo(JunctionBox other)
        {
            long dx = this.x - other.x;
            long dy = this.y - other.y;
            long dz = this.z - other.z;
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