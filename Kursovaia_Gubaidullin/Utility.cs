using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseMVC
{
    public class Pair
    {
        internal int x { get; }
        internal int y { get; }
        public Pair(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
    public class Trio
    {
        internal int x { get; }
        internal int y { get; }
        internal int z { get; }
        public Trio(int X, int Y, int Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
    public class HrefModel
    {
        public class type
        {
            public string Name;
            public string href;
        }
        public String Name;
        public List<type> Types;
    }
}
