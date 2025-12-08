namespace Advent2025
{
    public class Cooschmoordinate : IEquatable<Cooschmoordinate>, IComparable<Cooschmoordinate> //schmenum
    {
        public int x;
        public int y;
        public int z;
        public List<(Cooschmoordinate, float)> Friends;
        public HashSet<Cooschmoordinate> Lookup;
        public Cooschmoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            Friends = new List<(Cooschmoordinate, float)>();
            Lookup = new HashSet<Cooschmoordinate>();
            Lookup.Add(this);
        }
        public Cooschmoordinate(Cooschmoordinate c)
        {
            x = c.x;
            y = c.y;
            z = c.z;
            Friends = new List<(Cooschmoordinate, float)>();
            Lookup = new HashSet<Cooschmoordinate>();
            Lookup.Add(this);
        }
        public void AddTo(Cooschmoordinate A)
        {
            x += A.x;
            y += A.y;
            z += A.z;
            Friends = new List<(Cooschmoordinate, float)>();
            Lookup = new HashSet<Cooschmoordinate>();
            Lookup.Add(this);
        }
        public float DistantFriends(Cooschmoordinate c)
        {
            float deltaX = this.x - c.x; //copy paste from https://stackoverflow.com/questions/8914669/algorithm-for-calculating-a-distance-between-2-3-dimensional-points
            float deltaY = this.y - c.y; // I preferewd the top result because it makes me understand the wiki page better. Also someone said it was faster.
            float deltaZ = this.z - c.z;

            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            return distance;
        }
        public void AddFriend(Cooschmoordinate friend, float distance)
        {
            Friends.Add((friend, distance));
            Lookup.Add(friend);
        }
        public void PrioritizeFriends()
        {
            Friends = Friends.OrderBy(friend => friend.Item2).ToList();
        }
        //public void MoveNSteps(char c, int n = 1)
        //{
        //    switch (c)
        //    {
        //        case 'E':
        //        case 'R':
        //            this.AddTo(new Cooschmoordinate(1 * n, 0));
        //            break;
        //        case 'W':
        //        case 'L':
        //            this.AddTo(new Cooschmoordinate(-1 * n, 0));
        //            break;
        //        case 'N':
        //        case 'U':
        //            this.AddTo(new Cooschmoordinate(0, 1 * n));
        //            break;
        //        case 'S':
        //        case 'D':
        //            this.AddTo(new Cooschmoordinate(0, -1 * n));
        //            break;
        //    }
        //}
        public List<Cooschmoordinate> GetNeihbours()//bool Diagonals = false)
        {
            List<Cooschmoordinate> ReturnList = new List<Cooschmoordinate>();
            ReturnList.Add(this.GetSum(new Cooschmoordinate(1, 0, 0)));
            ReturnList.Add(this.GetSum(new Cooschmoordinate(-1, 0, 0)));
            ReturnList.Add(this.GetSum(new Cooschmoordinate(0, 1, 0)));
            ReturnList.Add(this.GetSum(new Cooschmoordinate(0, -1, 0)));
            ReturnList.Add(this.GetSum(new Cooschmoordinate(0, 0, -1)));
            ReturnList.Add(this.GetSum(new Cooschmoordinate(0, 0, 1)));
            //if (Diagonals)
            //{
            //    ReturnList.Add(this.GetSum(new Cooschmoordinate(1, 1)));
            //    ReturnList.Add(this.GetSum(new Cooschmoordinate(-1, -1)));
            //    ReturnList.Add(this.GetSum(new Cooschmoordinate(-1, 1)));
            //    ReturnList.Add(this.GetSum(new Cooschmoordinate(1, -1)));
            //}
            return ReturnList;
        }
        public Cooschmoordinate GetSum(Cooschmoordinate A)
        {
            int x2 = x + A.x;
            int y2 = y + A.y;
            int z2 = z + A.z;
            return new Cooschmoordinate(x2, y2, z2);
        }
        public bool IsInPositiveishBounds(int x2, int y2, int z2)
        {
            return (x >= -1 && y >= -1 && z >= -1 && x <= x2 && y <= y2 && z <= z2);
        }
        //public int ManhattanDistance(Cooschmoordinate coo)
        //{
        //    return Math.Abs(this.x - coo.x) + Math.Abs(this.y - coo.y);
        //}
        //public Cooschmoordinate RelativePosition(Cooschmoordinate coo)
        //{
        //    return new Cooschmoordinate(this.x - coo.x, this.y - coo.y);
        //}
        //public Position GetPosition()
        //{
        //    return new Position(x, y);
        //}
        public override string ToString()
        {
            return x.ToString() + "," + y.ToString() + "," + z.ToString();
        }

        public override int GetHashCode()
        {
            int hCode = x ^ y ^ z;
            return hCode.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Cooschmoordinate);
        }
        public bool Equals(Cooschmoordinate obj)
        {
            return obj != null && obj.x == x && obj.y == y && obj.z == z;
        }
        public int CompareTo(Cooschmoordinate other)
        {
            if (this.x == other.x)
            {
                if (this.y == other.y)
                {
                    return this.z.CompareTo(other.z);
                }
                return this.y.CompareTo(other.y);
            }
            return this.x.CompareTo(other.x);
        }
        //public void Assimilate(Cooschmoordinate c)
        //{
        //    x = c.x;
        //    y = c.y;
        //}
    }
    class CooschmoordinateEqualityComparer : IEqualityComparer<Cooschmoordinate>
    {
        public bool Equals(Cooschmoordinate b1, Cooschmoordinate b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.x == b2.x && b1.y == b2.y && b1.z == b2.z)
                return true;
            else
                return false;
        }

        public int GetHashCode(Cooschmoordinate bx)
        {
            int hCode = bx.x ^ bx.y ^ bx.z;
            return hCode.GetHashCode();
        }
    }
}