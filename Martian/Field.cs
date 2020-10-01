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

        public Field(Point topRightpoint)
        {
            width = topRightpoint.x;
            height = topRightpoint.y;
        }

        public static Field GetField(string fieldParams)
        {
            try
            {
                string[] locationParams = fieldParams.Split(' ');
                int x = Int32.Parse(locationParams[0]);
                int y = Int32.Parse(locationParams[1]);

                return new Field(new Point(x, y));
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
