using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Class
{
    public class UserStats
    {
        public int Wins;
        public int Loses;
        public int Total { get { return Wins + Loses; } }
        public int Points;
        public int Gold;
    }
}
