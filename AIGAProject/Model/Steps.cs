using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    class Step
    {
        const int MOVE_COUNT_MIN = 0;
        const int MOVE_COUNT_MAX = 10;
        public Direction Direction
        {
            get; set;
        }

        public int Count
        {
            get; set;
        }

        public Step()
        {
            var rand = new Random();
            Direction = (Direction)rand.Next(Enum.GetNames(typeof(Direction)).Length);
            Count = rand.Next(MOVE_COUNT_MIN, MOVE_COUNT_MAX);
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

        public List<Step> FullSteps
        {
            get
            {
                return _steps;
            }
        }

        public Steps(int counts)
        {
            for (int i = 0; i < counts; i++)
            {       
                _steps.Add(new Step());
            }
        }

        public Steps()
        {

        }

        public void Add(Step step)
        {
            _steps.Add(step);
        }

        public void SetStep(int index, Step step)
        {
            _steps[index] = step;
        }
    }
}
