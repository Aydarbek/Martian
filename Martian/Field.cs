using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class Field
    {
        public int width { get; }
        public int height { get; }
        public List<Point> protectedPoints { get; set; } = new List<Point>();

        public Field(Point point)
        {
            width = point.x;
            height = point.y;
        }
    }
}
