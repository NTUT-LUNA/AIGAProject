using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    class Robot
    {
        Point _location;
        public Point Location
        {
            get
            {
                return _location;
            }
        }

        Steps _steps;

        public Steps Steps
        {
            get
            {
                return _steps;
            }
        }

        public Robot(Point startLocation, int stepCounts)
        {
            _location = startLocation;
            _steps = new Steps(stepCounts); //亂數
        }

        public Robot(Point startLocation, Steps steps)
        {
            _location = startLocation;
            _steps = steps;
        }

        public void StartToMove(Map map)
        {
            for (int nowDoingStepNo = 0; nowDoingStepNo < _steps.Count; nowDoingStepNo++)
            {
                Step step = _steps.GetStep(nowDoingStepNo);
                _location = map.GetLocationResult(Location, step);
            }
        }
    }

    class Robots
    {
        List<Robot> _robotList = new List<Robot>();
        int _stepCounts;
        public List<Robot> RobotList
        {
            get
            {
                return _robotList;
            }
        }

        public Robots(Point startLocation, int robotCounts, int stepCounts)
        {
            _stepCounts = stepCounts;
            for (int i = 0; i < robotCounts; i++)
            {
                _robotList.Add(new Robot(startLocation, stepCounts));
            }
        }

        public void Evaluate(Point goalPoint)
        {
            List<Robot> nextGeneration = new List<Robot>();
            RobotResults robotResults = new RobotResults(_robotList, goalPoint, _stepCounts);
            _robotList = robotResults.GetNextGenerations();
        }
    }

    class RobotResults
    {
        List<RobotResult> robotResults = new List<RobotResult>();
        List<Robot> lastGeneration;
        Point goalPoint;
        int _stepCounts;
        public RobotResults(List<Robot> list, Point goal, int stepCounts)
        {
            lastGeneration = list;
            goalPoint = goal;
            _stepCounts = stepCounts;
            for (int i = 0; i < list.Count; i++)
            {
                robotResults.Add(new RobotResult(i, list[i].Location));
            }
        }

        //目前只看最短路徑
        public List<Robot> GetNextGenerations()
        {
            List<Robot> nextGenerationParents = new List<Robot>();
            int count = lastGeneration.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int index = FindMinDistanceRobotIndex();
                nextGenerationParents.Add(lastGeneration[index]);
                lastGeneration.RemoveAt(index);
            }

            List<Robot> nextGeneration = new List<Robot>();
            nextGeneration = Reproduce(nextGenerationParents, _stepCounts);
            nextGeneration = Mutation(nextGeneration);
            return nextGeneration;
        }

        int FindMinDistanceRobotIndex()
        {
            Point point = new Point(0, 0);
            int maxRobotIndex = -1;
            foreach(RobotResult result in robotResults)
            {
                if (GetDistance(point, goalPoint) > GetDistance(result.Location, goalPoint))
                {
                    point = result.Location;
                    maxRobotIndex = result.Index;
                }
            }
            return maxRobotIndex;
        }

        List<Robot> Reproduce(List<Robot> parents, int stepCounts)
        {
            List<Robot> childs = new List<Robot>();
            int count = parents.Count;

            for (int i = 0; i < count; i++)
            {
                if (i != count - 1)
                {
                    childs.Add(Crossover(parents[i], parents[i + 1], stepCounts));
                } else //尾跟頭
                {
                    childs.Add(Crossover(parents[i], parents[0], stepCounts));
                }
            }
        }

        Robot Crossover(Robot papa, Robot mama, int stepCounts)
        {
            int midPoint = stepCounts / 2;
            Steps newSteps = new Steps();
        }

        double GetDistance(Point p1, Point p2)
        {
            int x = Math.Abs(p1.X - p2.X);
            int y = Math.Abs(p1.Y - p2.Y);
            return Math.Abs(x ^ 2 - y ^ 2); //畢氏
        }
    }
}
