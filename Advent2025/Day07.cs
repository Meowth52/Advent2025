namespace Advent2025
{
    public class Day07 : Day
    {
        List<string> Instructions;
        public Day07(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Instructions = this.ParseStringArray(Input).ToList();
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0; ;
            HashSet<int> beams = new HashSet<int>();
            for (int i = 0; i < Instructions[0].Length; i++)
            {
                if (Instructions[0][i] == 'S')
                {
                    beams.Add(i);
                }
            }
            foreach (string instruction in Instructions)
            {
                HashSet<int> next = new HashSet<int>();
                foreach (int beam in beams)
                {
                    if (instruction[beam] == '^')
                    {
                        next.Add(beam - 1);
                        next.Add(beam + 1);
                        ReturnValue++;
                    }
                    else
                    {
                        next.Add(beam);
                    }
                }
                beams = new HashSet<int>(next);
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 1;
            Dictionary<int, long> beams = new Dictionary<int, long>();
            for (int i = 0; i < Instructions[0].Length; i++)
            {
                if (Instructions[0][i] == 'S')
                {
                    beams.Add(i, 1);
                }
            }
            foreach (string instruction in Instructions)
            {
                Dictionary<int, long> next = new Dictionary<int, long>();
                foreach (int beam in beams.Keys)
                {
                    if (instruction[beam] == '^')
                    {
                        if (!next.ContainsKey(beam + 1))
                            next[beam + 1] = beams[beam];
                        else
                            next[beam + 1] += beams[beam];
                        if (!next.ContainsKey(beam - 1))
                            next[beam - 1] = beams[beam];
                        else
                            next[beam - 1] += beams[beam];
                    }
                    else if (!next.ContainsKey(beam))
                    {
                        next[beam] = beams[beam];
                    }
                    else
                        next[beam] += beams[beam];
                }
                beams = new Dictionary<int, long>(next);
            }
            ReturnValue = beams.Values.Sum();
            return ReturnValue.ToString();
        }
    }
}
