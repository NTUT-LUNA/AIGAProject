using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    class Robot
    {
        Point _location;
        Point _startLocation;
        int _nowDoingStepNo = 0;

        public Point Location
        {
            get
            {
                return _location;
            }
        }

        public int Radius
        {
            get;
        }

        public Step NowDoingStep
        {
            get
            {
                return GetStep(_nowDoingStepNo);
            }
        }

        public Point NextLocation
        {
            get
            {
                return _location.NextLocation(NowDoingStep.Direction);
            }
        }

        Steps _steps;

        public Robot(Point startLocation, int radius, int stepCounts)
        {
            Radius = radius;
            _startLocation = startLocation;
            _location = startLocation;
            _steps = new Steps(stepCounts); //亂數
        }

        public Robot(Point startLocation, int radius, Steps steps)
        {
            Radius = radius;
            _startLocation = startLocation;
            _location = startLocation;
            _steps = steps;
        }

        public void MoveTillTheEnd(Map map)
        {
            for (int i = _nowDoingStepNo; i < _steps.Count; i++)
            {
                MoveOnce(map);
            }
        }

        public void MoveOnce(Map map)
        {
            if (map.LocationVaild(NextLocation, Radius))
            {
                _location = NextLocation;
            }
            _nowDoingStepNo++;
        }

        public Step GetStep(int index)
        {
            return _steps.GetStep(index);
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

        public void Reset()
        {
            _nowDoingStepNo = 0;
            _location = _startLocation;
        }
    }

    class Robots
    {
        List<Robot> _robotList = new List<Robot>();
        Point _startLocation;
        int _stepCounts;
        int _robotRadius;
        Map _map;

        public Robots(Point startLocation, int robotRadius, int robotCounts, int stepCounts)
        {
            _robotRadius = robotRadius;
            _startLocation = startLocation;
            _stepCounts = stepCounts;
            for (int i = 0; i < robotCounts; i++)
            {
                _robotList.Add(new Robot(startLocation, robotRadius, stepCounts));
            }
        }

        public void StartToMove()
        {
            foreach (Robot robot in _robotList)
            {
                robot.MoveTillTheEnd(_map);
            }
        }

        //剔除前 50% 離最遠的傢伙
        public void Selection(SelectMode mode)
        {
            //不知道這個 switch 可以怎麼多型?
            switch (mode)
            {
                case SelectMode.Distance:
                    SelectDistance();
                    break;
                case SelectMode.Area:
                    break;
            }
        }

        void SelectDistance()
        {
            int count = _robotList.Count;
            for (int i = 0; i < count / 2; i++)
            {
                int index = FindMaxDistanceRobotIndex(_map.GoalPoint);
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
            return new Robot(_startLocation, _robotRadius, newSteps);
        }

        public void Mutation(double mutationRate)
        {
            foreach (Robot robot in _robotList)
            {
                robot.Mutation(mutationRate);
            }
        }

        public void Reset()
        {
            foreach (Robot robot in _robotList)
            {
                robot.Reset();
            }
        }
    }
}
