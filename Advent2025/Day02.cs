namespace Advent2025
{
    public class Day02 : Day
    {
        List<(long, long)> Ranges;
        public Day02(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input, ",");
            Ranges = new List<(long, long)>();
            foreach (string s in strings)
            {
                List<long> longs = this.ParseListOfLong(s.Replace("-", ","));
                Ranges.Add((longs[0], longs[1]));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            List<(long, long)> eh = new List<(long, long)>();
            foreach ((long, long) range in Ranges)
            {
                int lowerD = GetDigits(range.Item1);
                int higherD = GetDigits(range.Item2);
                long lower = range.Item1;
                long higher = range.Item2;
                if (higherD % 2 == 0 || lowerD % 2 == 0)
                {
                    if (lowerD % 2 != 0)
                        lower = (long)Math.Pow(10, lowerD);
                    if (higherD % 2 != 0)
                        higher = (long)Math.Pow(10, higherD - 1) - 1;
                    long from = GetLeftHalf(lower);
                    long to = GetLeftHalf(higher);
                    for (long i = from; i < to; i++)
                    {
                        long palindromifiedd = Palimdromify(i);
                        if (palindromifiedd >= lower && palindromifiedd <= higher)
                        {
                            ReturnValue += palindromifiedd;
                        }
                    }
                    long palindromified = Palimdromify(to);
                    if (palindromified >= lower && palindromified <= higher)
                    {
                        ReturnValue += palindromified;
                    }
                }

            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            List<(long, long)> eh = new List<(long, long)>();
            foreach ((long, long) range in Ranges)
            {
                HashSet<long> returnSet = new HashSet<long>();
                int lowerD = GetDigits(range.Item1);
                int higherD = GetDigits(range.Item2);
                for (int n = 2; n <= higherD; n++)
                {
                    long lower = range.Item1;
                    long higher = range.Item2;
                    if (n == 3 && lower == 95)
                        ;
                    if (higherD % n == 0 || lowerD % n == 0)
                    {
                        if (lowerD % n != 0)
                            lower = (long)Math.Pow(10, lowerD);
                        if (higherD % n != 0)
                            higher = (long)Math.Pow(10, higherD - 1) - 1;
                        long from = GetLeftN(lower, n);
                        long to = GetLeftN(higher, n);
                        for (long i = from; i < to; i++)
                        {
                            long palindromifiedd = Palimdromifyier(i, n);
                            if (palindromifiedd >= lower && palindromifiedd <= higher)
                            {
                                returnSet.Add(palindromifiedd);
                            }
                        }
                        long palindromified = Palimdromifyier(to, n);
                        if (palindromified >= lower && palindromified <= higher)
                        {
                            returnSet.Add(palindromified);
                        }
                    }
                }
                ReturnValue += returnSet.Sum();
            }
            return ReturnValue.ToString();
        }
        public int GetDigits(long number)
        {
            int digits = (int)(Math.Floor(Math.Log10(number)) + 1);
            return digits;
        }
        public long GetLeftHalf(long number)
        {
            int half = GetDigits(number) / 2;
            long returnValue = number / (long)Math.Pow(10, half);
            return returnValue;
        }
        public long GetLeftN(long number, int n)
        {
            int digits = GetDigits(number);
            int half = digits - digits / n;
            long returnValue = number / (long)Math.Pow(10, half);
            return returnValue;
        }
        public long Palimdromify(long number)
        {
            long returnValue = (number * (long)Math.Pow(10, GetDigits(number))) + number;
            return returnValue;
        }
        public long Palimdromifyier(long number, int n)
        {
            long wipNumber = number;
            long f = GetDigits(number);
            for (int i = 1; i < n; i++)
            {
                wipNumber = (wipNumber * (long)Math.Pow(10, f)) + number;
            }
            return wipNumber;
        }
    }
}
