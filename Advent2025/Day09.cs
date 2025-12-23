namespace Advent2025
{
    public class Day09 : Day
    {
        List<Cooooordinate> Coordinates;
        public Day09(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            Coordinates = new List<Cooooordinate>();
            string[] strings = this.ParseStringArray(Input);
            foreach (string s in strings)
            {
                List<long> longs = this.ParseListOfLong(s);
                Coordinates.Add(new Cooooordinate(longs[0], longs[1]));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            foreach (Cooooordinate c in Coordinates)
            {
                foreach (Cooooordinate other in Coordinates)
                {
                    long x = Math.Abs(c.x - other.x) + 1;
                    long y = Math.Abs(c.y - other.y) + 1;
                    long area = x * y;
                    if (area > ReturnValue)
                        ReturnValue = area;
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            foreach (Cooooordinate c in Coordinates)
            {
                foreach (Cooooordinate other in Coordinates)
                {
                    bool nope = false;
                    long x = Math.Abs(c.x - other.x) + 1;
                    long y = Math.Abs(c.y - other.y) + 1;
                    foreach (Cooooordinate thirdier in Coordinates)
                    {
                        if (!((thirdier.x == c.x || thirdier.x == other.x) && (thirdier.y == c.y || thirdier.y == other.y)) && thirdier.IsBetween(c, other, inclusive: true))
                        {
                            if (thirdier.IsBetween(c, other, inclusive: false))
                            {
                                nope = true;
                                break;
                            }
                            else
                            {
                                Cooooordinate aligned = new Cooooordinate(c);
                                Cooooordinate unaligned = new Cooooordinate(other);
                                if (thirdier.x == other.x || thirdier.y == other.y)
                                {
                                    aligned = new Cooooordinate(other);
                                    unaligned = new Cooooordinate(c);
                                }
                                int index = Coordinates.IndexOf(aligned);
                                if (index == 0)
                                    ;
                                int iPlus = index + 1;
                                if (iPlus >= Coordinates.Count)
                                    iPlus = 0;
                                int iMinus = index - 1;
                                if (iMinus < 0)
                                    iMinus = Coordinates.Count - 1;
                                if (thirdier.x == aligned.x)
                                {
                                    Cooooordinate fourhest = new Cooooordinate(Coordinates[iPlus]);
                                    if (Coordinates[iMinus] != thirdier)
                                    {
                                        fourhest = new Cooooordinate(Coordinates[iMinus]);
                                    }
                                    List<long> between = new List<long> { unaligned.y, fourhest.y };
                                    between.Sort();
                                    if (thirdier.y > between[0] && thirdier.y < between[1])
                                    {
                                        nope = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    Cooooordinate fourhest = new Cooooordinate(Coordinates[iPlus]);
                                    if (Coordinates[iMinus] != thirdier)
                                    {
                                        fourhest = new Cooooordinate(Coordinates[iMinus]);
                                    }
                                    List<long> between = new List<long> { unaligned.x, fourhest.x };
                                    between.Sort();
                                    if (thirdier.x > between[0] && thirdier.x < between[1])
                                    {
                                        nope = true;
                                        break;
                                    }

                                }
                            }
                        }
                    }
                    if (!nope)
                    {
                        long area = x * y;
                        if (area > ReturnValue)
                            ReturnValue = area;
                    }
                }
            }

            return ReturnValue.ToString();
        }
    }
}
