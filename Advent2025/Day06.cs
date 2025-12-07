namespace Advent2025
{
    public class Day06 : Day
    {
        List<List<long>> Numbers;
        List<List<long>> MenVaFan;
        List<char> Operators;
        public Day06(string _input) : base(_input)
        {
            string Input = this.CheckFile(_input);
            string[] strings = this.ParseStringArray(Input);
            string[] rawStrings = Input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Operators = strings.Last().Replace(" ", "").ToCharArray().ToList();
            Numbers = new List<List<long>>();
            for (int i = 0; i < strings.Length - 1; i++)
            {
                Numbers.Add(this.ParseListOfLong(strings[i]));
            }
            string bläcksiskjävlar = "";
            MenVaFan = new List<List<long>>();
            for (int i = rawStrings[0].Length - 1; i >= 0; i--)
            {

                for (int n = 0; n < rawStrings.Length; n++)
                {
                    char character = rawStrings[n][i];
                    if (character == '+' || character == '*')
                    {
                        MenVaFan.Add(this.ParseListOfLong(bläcksiskjävlar));
                        bläcksiskjävlar = "";
                    }
                    else
                    {
                        bläcksiskjävlar += character;
                    }
                }
                bläcksiskjävlar += " ";
            }
        }
        public override Tuple<string, string> GetResult()
        {
            return Tuple.Create(GetPartOne(), GetPartTwo());
        }
        public string GetPartOne()
        {
            long ReturnValue = 0;
            for (int i = 0; i < Operators.Count; i++)
            {
                if (Operators[i] == '+')
                {
                    foreach (List<long> column in Numbers)
                    {
                        ReturnValue += column[i];
                    }
                }
                if (Operators[i] == '*')
                {
                    long multiplied = 1;
                    foreach (var column in Numbers)
                    {
                        multiplied *= column[i];
                    }
                    ReturnValue += multiplied;
                }
            }
            return ReturnValue.ToString();
        }
        public string GetPartTwo()
        {
            long ReturnValue = 0;
            for (int i = 0; i < Operators.Count; i++)
            {
                int index = (Operators.Count - 1) - i;
                if (Operators[i] == '+')
                {
                    foreach (long n in MenVaFan[index])
                    {
                        ReturnValue += n;
                    }
                }
                if (Operators[i] == '*')
                {
                    long multiplied = 1;
                    foreach (long n in MenVaFan[index])
                    {
                        multiplied *= n;
                    }
                    ReturnValue += multiplied;
                }

            }
            return ReturnValue.ToString();
        }
    }
}
