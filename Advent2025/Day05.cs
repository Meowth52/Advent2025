namespace Advent2025
{
    public class Day05 : Day
    {
        List<(long, long)> Ranges;
        List<long> Ingridients;
        public Day05(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] separated = Input.Split("\r\n\r\n");
            Ranges = new List<(long, long)>();
            separated[0] = separated[0].Replace('-', ',');
            string[] strings = this.ParseStringArray(separated[0], "\r\n");
            foreach (string s in strings)
            {
                List<long> longs = this.ParseListOfLong(s);
                Ranges.Add((longs[0], longs[1]));
            }
            Ingridients = this.ParseListOfLong(separated[1]);
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (long ingridient in Ingridients)
            {
                foreach ((long, long) range in Ranges)
                {
                    if (ingridient >= range.Item1 && ingridient <= range.Item2)
                    {
                        ReturnValue++;
                        break;
                    }
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            Queue<(long, long)> rangeQueue = new Queue<(long, long)>(Ranges);
            List<(long, long)> done = new List<(long, long)>();
            while (rangeQueue.Count > 0)
            {
                List<(long, long)> next = new List<(long, long)>();
                (long, long) range = rangeQueue.Dequeue();
                bool modified = false;
                foreach (var r in rangeQueue)
                {
                    if (range.Item1 > r.Item2 || range.Item2 < r.Item1)
                    {
                        next.Add(r);
                    }
                    else
                    {
                        if (range.Item1 > r.Item1)
                        {
                            modified = true;
                            range.Item1 = r.Item1;
                        }
                        if (range.Item2 < r.Item2)
                        {
                            modified = true;
                            range.Item2 = r.Item2;
                        }
                    }
                }
                if (modified)
                    next.Add(range);
                else
                    done.Add(range);
                rangeQueue = new Queue<(long, long)>(next);
            }
            foreach ((long, long) range in done)
            {
                ReturnValue += (range.Item2 - range.Item1 + 1);
            }
            return ReturnValue.ToString();
        }
    }
}
