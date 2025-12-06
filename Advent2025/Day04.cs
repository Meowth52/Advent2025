namespace Advent2025
{
    public class Day04 : Day
    {
        Dictionary<Coordinate, char> Map;
        public Day04(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Map = this.ParseCoordinateCharDic(Input, '.');
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (Coordinate roll in Map.Keys)
            {
                int nrOfNeighbours = 0;
                HashSet<Coordinate> neighbours = roll.GetNeihbours(Diagonals: true).ToHashSet();
                foreach (Coordinate neighbour in neighbours)
                {
                    if (Map.ContainsKey(neighbour))
                        nrOfNeighbours++;
                }
                if (nrOfNeighbours < 4)
                    ReturnValue++;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            HashSet<Coordinate> rolls = Map.Keys.ToHashSet();
            while (true)
            {
                HashSet<Coordinate> next = new HashSet<Coordinate>();
                foreach (Coordinate roll in rolls)
                {
                    int nrOfNeighbours = 0;
                    HashSet<Coordinate> neighbours = roll.GetNeihbours(Diagonals: true).ToHashSet();
                    foreach (Coordinate neighbour in neighbours)
                    {
                        if (rolls.Contains(neighbour))
                            nrOfNeighbours++;
                    }
                    if (nrOfNeighbours >= 4)
                        next.Add(roll);

                }
                if (next.Count == rolls.Count)
                    break;
                rolls = new HashSet<Coordinate>(next);
            }
            ReturnValue = Map.Count - rolls.Count;
            return ReturnValue.ToString();
        }
    }
}
