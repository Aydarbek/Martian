using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            Point point = (Point)obj;
            return point.x == this.x && point.y == this.y;
        }

        public override string ToString()
        {
            return x + " " + y;
        }
    }
}
