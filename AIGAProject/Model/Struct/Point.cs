using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGAProject.Model
{
    class Point
    {
        public int X
        {
            get; set;
        }

        public int Y
        {
            get; set;
        }

        public Point(int x1, int y1)
        {
            X = x1;
            Y = y1;
        }

        public Point Copy()
        {
            return new Point(X, Y);
        }

        public Point NextLocation(Direction direction)
        {
            Point nextPoint = Copy();
            switch (direction)
            {
                case Direction.N:
                    nextPoint.Y++;
                    break;
                case Direction.NE:
                    nextPoint.Y++;
                    nextPoint.X++;
                    break;
                case Direction.E:
                    nextPoint.X++;
                    break;
                case Direction.SE:
                    nextPoint.Y--;
                    nextPoint.X++;
                    break;
                case Direction.S:
                    nextPoint.Y--;
                    break;
                case Direction.SW:
                    nextPoint.Y--;
                    nextPoint.X--;
                    break;
                case Direction.W:
                    nextPoint.X--;
                    break;
                case Direction.NW:
                    nextPoint.Y++;
                    nextPoint.X--;
                    break;
            }
            return nextPoint;
        }
    }
}
