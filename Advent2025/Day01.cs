
using System.Text.RegularExpressions;


namespace Advent2025
{
    public class Day01 : Day
    {
        List<(char, int)> Turns;
        public Day01(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            Turns = new List<(char, int)>();
            foreach (string s in strings)
            {
                string intString = Regex.Matches(s, @"-?\d+").First().Value;
                Turns.Add((s[0], Int32.Parse(intString)));
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            int ReturnValue = 0;
            int dial = 50;
            foreach ((char, int) turn in Turns)
            {
                if (turn.Item1 == 'R')
                {
                    dial = (dial + turn.Item2) % 100;
                }
                else if (turn.Item1 == 'L')
                {
                    dial = (dial - turn.Item2) % 100;
                    if (dial < 0)
                        dial += 100;
                }
                else
                {
                    throw new Exception("Invalid turn");
                }
                if (dial == 0)
                    ReturnValue++;
                ;
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            int ReturnValue = 0;
            int dial = 50;
            foreach ((char, int) turn in Turns)
            {
                if (turn.Item1 == 'R')
                {
                    dial = (dial + turn.Item2);
                    ReturnValue += dial / 100;
                    dial %= 100;
                }
                else if (turn.Item1 == 'L')
                {
                    if (dial == 0)
                        ReturnValue--; //It feels easiest to just take one away and count it again
                    dial = (dial - turn.Item2);
                    ReturnValue += -dial / 100;
                    dial %= 100;
                    if (dial == 0)
                        ReturnValue++;
                    if (dial < 0)
                    {
                        dial += 100;
                        ReturnValue++;
                    }
                }
                else
                {
                    throw new Exception("Invalid turn");
                }
                ;
            }
            return ReturnValue.ToString();
        }
    }
}
