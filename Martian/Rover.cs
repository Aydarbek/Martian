using Martian.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class Rover
    {
        public Point position { get; set; }
        public Direction direction { get; set; }
        public bool lost { get; set; } = false;
        internal IRudderState rudderState { get; set; }
        

        internal NorthDirection northDirection;
        internal WestDirection westDirection;
        internal EastDirection eastDirection;
        internal SouthDirection southDirection;


        public Rover(Point startPoint, Direction startDirection)
        {
            position = startPoint;
            direction = startDirection;
            CreateRudderStates();
            SetRudderState();
        }

        private void CreateRudderStates()
        {
            northDirection = new NorthDirection(this);
            westDirection = new WestDirection(this);
            eastDirection = new EastDirection(this);
            southDirection = new SouthDirection(this);
        }

        private void SetRudderState()
        {
            switch (direction)
            {
                case (Direction.N): 
                    rudderState = northDirection;
                    break;
                case (Direction.E):
                    rudderState = eastDirection;
                    break;
                case (Direction.S):
                    rudderState = southDirection;
                    break;
                case (Direction.W):
                    rudderState = westDirection;
                    break;
                default:
                    rudderState = northDirection;
                    break;
            }
        }

        public override string ToString()
        {
            string result = position.x + " " + position.y + " " + direction.ToString();
            if (lost)
                result += " LOST";

            return result;
        }
    }    
}
