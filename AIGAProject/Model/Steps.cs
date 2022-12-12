using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    class Step
    {
        public Direction Direction
        {
            get; set;
        }

        public int Count
        {
            get; set;
        }
    }

    class Steps
    {
        List<Step> steps = new List<Step>();

        public int Count
        {
            get
            {
                return steps.Count;
            }
        }

        public Steps(int counts)
        {
            for (int i = 0; i < counts; i++)
            {
                var rand = new Random();
                Step step = new Step();
                step.Direction = (Direction)rand.Next(Enum.GetNames(typeof(Direction)).Length);
                step.Count = rand.Next(0, 10);
                steps.Add(step);
            }
        }

        public Steps()
        {

        }

        public void Add(Step step)
        {

        }

        public Step GetStep(int index)
        {
            return steps[index];
        }

    }
}
