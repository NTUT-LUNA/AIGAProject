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
                Step step = _steps.FullSteps[nowDoingStepNo];
                _location = map.GetLocationResult(Location, step);
            }
        }

        public Step GetStep(int index)
        {
            return _steps.FullSteps[index];
        }

        public void Mutation(double mutationRate)
        {
            //每一個 Step 獨立計算是否突變
            var rand = new Random();
            for (int i = 0; i < _steps.Count; i++)
            {
                if (rand.NextDouble() < mutationRate) //發生突變
                {
                    Step step = new Step();
                    _steps.SetStep(i, step);
                }
            }
        }
    }

    class Robots
    {
        List<Robot> _robotList = new List<Robot>();
        Point _startLocation;
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
            _startLocation = startLocation;
            _stepCounts = stepCounts;
            for (int i = 0; i < robotCounts; i++)
            {
                _robotList.Add(new Robot(startLocation, stepCounts));
            }
        }

        public void StartToMove(Map map)
        {
            foreach (Robot robot in _robotList)
            {
                robot.StartToMove(map);
            }
        }

        //剔除前 50% 離最遠的傢伙
        public void Selection(Point goalPoint)
        {
            int count = _robotList.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int index = FindMaxDistanceRobotIndex(goalPoint);
                _robotList.RemoveAt(index);
            }
        }

        int FindMaxDistanceRobotIndex(Point goalPoint)
        {
            List<RobotResult> robotResults = new List<RobotResult>();
            for (int i = 0; i < _robotList.Count; i++)
            {
                robotResults.Add(new RobotResult(i, _robotList[i].Location));
            }
            Point point = new Point(int.MinValue, int.MinValue);
            int maxRobotIndex = 0;
            foreach (RobotResult result in robotResults)
            {
                if (GetDistance(point, goalPoint) < GetDistance(result.Location, goalPoint))
                {
                    point = result.Location;
                    maxRobotIndex = result.Index;
                }
            }
            return maxRobotIndex;
        }

        double GetDistance(Point p1, Point p2)
        {
            int x = Math.Abs(p1.X - p2.X);
            int y = Math.Abs(p1.Y - p2.Y);
            return Math.Abs(x ^ 2 - y ^ 2); //畢氏
        }

        public void Crossover()
        {
            List<Robot> childs = new List<Robot>();

            for (int i = 0; i < _robotList.Count; i++)
            {
                if (i != _robotList.Count - 1)
                {
                    childs.Add(Crossover(_robotList[i], _robotList[i + 1], _stepCounts));
                }
                else //尾跟頭
                {
                    childs.Add(Crossover(_robotList[i], _robotList[0], _stepCounts));
                }
            }

            _robotList.AddRange(childs);
        }

        Robot Crossover(Robot robotPapa, Robot robotMama, int stepCounts)
        {
            int midPoint = stepCounts / 2;
            Steps newSteps = new Steps();
            for (int i = 0; i < midPoint; i++)
            {
                newSteps.Add(robotPapa.GetStep(i));
            }
            for (int i = midPoint; i < stepCounts; i++)
            {
                newSteps.Add(robotMama.GetStep(i));
            }
            return new Robot(_startLocation, newSteps);
        }

        public void Mutation(double mutationRate)
        {
            foreach (Robot robot in _robotList)
            {
                robot.Mutation(mutationRate);
            }
        }
    }
}
