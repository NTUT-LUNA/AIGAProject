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
        List<Step> _steps = new List<Step>();

        public int Count
        {
            get
            {
                return _steps.Count;
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
                _steps.Add(step);
            }
        }

        public Steps()
        {

        }

        public void Add(Step step)
        {
            _steps.Add(step);
        }

        public Step GetStep(int index)
        {
            return _steps[index];
        }

    }
}
