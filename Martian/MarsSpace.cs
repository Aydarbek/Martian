using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class MarsSpace
    {
        public int width { get; }
        public int height { get; }
        public ArrayList protectedPoints { get; set; }

        public MarsSpace(Point point)
        {
            width = point.x;
            height = point.y;
        }
    }
}
