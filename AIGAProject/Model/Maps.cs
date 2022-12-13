using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIGAProject.Model
{
    class Map
    {
        //老師給定的，寫死
        const int EMPTY_CELL = 0;
        const int OBSTACLE_CELL = 1;
        const int START_CELL = 2;
        const int GOAL_CELL = 3;
        int[,] mapArray;

        public int Height
        {
            get;
        }

        public int Width
        {
            get;
        }
        public Point StartPoint
        {
            get;
        }

        public Point GoalPoint
        {
            get;
        }

        bool IsObstacle(Point point)
        {
            return mapArray[point.Y, point.X] == OBSTACLE_CELL;
        }

        Point MoveNorthWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.Y++;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveNorthEastWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X++;
                nextPoint.Y++;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveEastWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X++;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveSouthEastWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X++;
                nextPoint.Y--;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveSouthWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.Y--;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveSouthWestWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X--;
                nextPoint.Y--;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveWestWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X--;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        Point MoveNorthWestWay(Point startLocation, Step step)
        {
            Point point = startLocation;
            for (int i = 0; i < step.Count; i++)
            {
                Point nextPoint = point;
                nextPoint.X--;
                nextPoint.Y++;
                if (OutOfBound(nextPoint))
                {
                    return point;
                }
                if (!IsObstacle(nextPoint))
                {
                    point = nextPoint;
                }
            }
            return point;
        }

        bool OutOfBound(Point p)
        {
            return (p.X < 0 ||
                    p.Y < 0 ||
                    p.X >= Width ||
                    p.Y >= Height);
        }

        public Point GetLocationResult(Point startLocation, Step step)
        {
            switch (step.Direction)
            {
                case Direction.N:
                    return MoveNorthWay(startLocation, step);                    
                case Direction.NE:
                    return MoveNorthEastWay(startLocation, step);                    
                case Direction.E:
                    return MoveEastWay(startLocation, step);                    
                case Direction.SE:
                    return MoveSouthEastWay(startLocation, step);                    
                case Direction.S:
                    return MoveSouthWay(startLocation, step);                    
                case Direction.SW:
                    return MoveSouthWestWay(startLocation, step);                    
                case Direction.W:
                    return MoveWestWay(startLocation, step);                    
                case Direction.NW:
                    return MoveNorthWestWay(startLocation, step);                    
            }
            return startLocation;
        }

        public Map(string path)
        {
            using (var reader = new StreamReader(path))
            {
                int j = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        Height = values.Length;
                        Width = values.Length;
                        mapArray = new int[values.Length, values.Length];
                        mapArray[j, i] = int.Parse(values[i]);
                        switch (mapArray[j, i])
                        {
                            case START_CELL:
                                StartPoint = new Point(j, i);
                                break;
                            case GOAL_CELL:
                                GoalPoint = new Point(j, i);
                                break;
                        }
                    }
                    j++;
                }
            }
        }
    }

    class Maps
    {
        Map testMap = new Map("../../../../ai_map/mytest.csv");

        public Map TestMap
        {
            get
            {
                return testMap;
            }
        }
    }
}
