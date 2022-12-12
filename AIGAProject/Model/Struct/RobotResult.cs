using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    struct RobotResult
    {
        public int Index
        {
            get; set;
        }

        public Point Location
        {
            get; set;
        }

        public RobotResult(int i, Point p)
        {
            Index = i;
            Location = p;
        }
    }
}
