namespace Advent2025
{
    public class Day03 : Day
    {
        List<List<int>> Batteries;
        public Day03(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Batteries = new List<List<int>>();
            foreach (string sstring in strings)
            {
                List<int> battery = new List<int>();
                foreach (char c in sstring)
                {
                    battery.Add(int.Parse(c.ToString()));
                }
                Batteries.Add(battery);
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            foreach (List<int> battery in Batteries)
            {
                int jolt = 0;
                int förstaBästa = getFörstaBästa(battery, -1);
                jolt += battery[förstaBästa] * 10;
                List<int> notTheBest = battery.GetRange(förstaBästa + 1, battery.Count - (förstaBästa + 1));
                int andraBästa = getFörstaBästa(notTheBest);
                jolt += notTheBest[andraBästa];
                ReturnValue += jolt;
            }
            return ReturnValue.ToString();

        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;

            foreach (List<int> battery in Batteries)
            {
                long jolt = 0;
                List<int> diminishingBattery = new List<int>(battery);
                for (int i = 11; i >= 0; i--)
                {
                    int nBästa = getFörstaBästa(diminishingBattery, -i);
                    jolt += diminishingBattery[nBästa] * (long)Math.Pow(10, i);
                    diminishingBattery = diminishingBattery.GetRange(nBästa + 1, diminishingBattery.Count - (nBästa + 1));
                }
                ReturnValue += jolt;
            }
            return ReturnValue.ToString();
        }
        int getFörstaBästa(List<int> battery, int offset = 0)
        {
            List<int> returnList = new List<int>();
            for (int i = 9; i >= 0; i--)
            {
                if (battery.Contains(i) && battery.IndexOf(i) < battery.Count + offset)
                {
                    return battery.IndexOf(i);
                }
            }
            throw new Exception("No valid digit found");
        }
    }
}
