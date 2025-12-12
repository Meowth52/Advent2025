namespace Advent2025
{
    public class Day08 : Day
    {
        List<Cooschmoordinate> Boxes;
        public Day08(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] rows = this.ParseStringArray(Input);
            Boxes = new List<Cooschmoordinate>();
            foreach (string row in rows)
            {
                List<int> box = this.ParseListOfInteger(row);
                Boxes.Add(new Cooschmoordinate(box[0], box[1], box[2]));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 1;
            int iterations = 1000;
            if (Boxes.Count < 25) // the test data
            {
                iterations = 10;
            }
            foreach (Cooschmoordinate box in Boxes)
            {
                foreach (Cooschmoordinate friend in Boxes)
                {
                    if (!box.Lookup.Contains(friend))
                    {
                        float distanceBetweenFriends = box.DistantFriends(friend);
                        box.AddFriend(friend, distanceBetweenFriends);
                        friend.AddFriend(box, distanceBetweenFriends);
                    }
                }
            }
            foreach (Cooschmoordinate box in Boxes)
            {
                box.PrioritizeFriends();
            }
            HashSet<float> distancesH = new HashSet<float>();
            Dictionary<float, List<(Cooschmoordinate, Cooschmoordinate)>> distanceLookup = new Dictionary<float, List<(Cooschmoordinate, Cooschmoordinate)>>();
            foreach (Cooschmoordinate box in Boxes)
            {
                foreach ((Cooschmoordinate friend, float distance) in box.Friends)
                {
                    if (!distancesH.Contains(distance))
                    {
                        distancesH.Add(distance);
                        distanceLookup.Add(distance, new List<(Cooschmoordinate, Cooschmoordinate)>());
                    }
                    if (!distanceLookup[distance].Contains((box, friend)) && !distanceLookup[distance].Contains((friend, box)))
                    {
                        distanceLookup[distance].Add((box, friend));
                    }
                }
            }
            HashSet<(Cooschmoordinate, Cooschmoordinate)> done = new HashSet<(Cooschmoordinate, Cooschmoordinate)>();
            List<HashSet<Cooschmoordinate>> circuits = new List<HashSet<Cooschmoordinate>>();
            int connections = 0;
            List<float> distances = distancesH.ToList();
            distances.Sort();
            while (connections < iterations)
            {
                float distance = distances.First();
                Cooschmoordinate lonelyGuy = null;
                Cooschmoordinate canHasFriend = null;
                bool skip = true;
                foreach (var pair in distanceLookup[distance])
                {
                    if (!done.Contains(pair))
                    {
                        done.Add(pair);
                        done.Add((pair.Item2, pair.Item1));
                        lonelyGuy = pair.Item1;
                        canHasFriend = pair.Item2;
                        skip = false;
                        break;
                    }
                }
                if (skip)
                    continue;
                distanceLookup[distance].Remove((lonelyGuy, canHasFriend));
                if (distanceLookup[distance].Count() == 0)
                    distances.RemoveAt(0);
                bool found = false;
                bool doubleTrouble = false;
                int imJustSpammingVariablesAtThisPoint = 0;
                int andFirstOne = 0;
                int andAnotherOne = 0;
                bool connected = false;
                foreach (HashSet<Cooschmoordinate> circuit in circuits)
                {
                    if (circuit.Contains(lonelyGuy) || circuit.Contains(canHasFriend))
                    {
                        if (found)
                        {
                            doubleTrouble = true;
                            andAnotherOne = imJustSpammingVariablesAtThisPoint;

                            break;
                        }

                        if (!circuit.Contains(canHasFriend))
                        {
                            circuit.Add(canHasFriend);
                            connected = true;
                            connections++;
                        }
                        else if (!circuit.Contains(lonelyGuy))
                        {
                            circuit.Add(lonelyGuy);
                            connected = true;
                            connections++;
                        }
                        else
                            connections++;
                        found = true;
                        andFirstOne = imJustSpammingVariablesAtThisPoint;
                    }
                    imJustSpammingVariablesAtThisPoint++;
                }
                if (!found)
                {
                    HashSet<Cooschmoordinate> newCircuit = new HashSet<Cooschmoordinate>();
                    newCircuit.Add(lonelyGuy);
                    newCircuit.Add(canHasFriend);
                    circuits.Add(newCircuit);
                    connections++;
                }
                if (doubleTrouble)
                {
                    //if (connections > iterations)
                    //    break;
                    foreach (Cooschmoordinate coosch in circuits[andAnotherOne])
                        circuits[andFirstOne].Add(coosch);
                    circuits.RemoveAt(andAnotherOne);
                    if (!connected)
                        connections++;
                }
            }
            List<int> circuitSizes = new List<int>();
            foreach (HashSet<Cooschmoordinate> circuit in circuits)
            {
                circuitSizes.Add(circuit.Count);
            }
            circuitSizes.Sort((a, b) => b.CompareTo(a));
            for (int i = 0; i < 3; i++)
            {
                ReturnValue *= circuitSizes[i];
            }
            return ReturnValue.ToString();

        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;

            return ReturnValue.ToString();
        }
    }
}
