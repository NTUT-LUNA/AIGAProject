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

        public bool LocationVaild(Point nextPoint, int robotRadius)
        {
            return (!OutOfBound(nextPoint, robotRadius) && !IsObstacle(nextPoint, robotRadius));
        }

        bool IsObstacle(Point point, int robotRadius)
        {
            return  mapArray[point.Y + robotRadius, point.X] == OBSTACLE_CELL ||
                    mapArray[point.Y + robotRadius, point.X + robotRadius]  == OBSTACLE_CELL ||
                    mapArray[point.Y, point.X + robotRadius]  == OBSTACLE_CELL ||
                    mapArray[point.Y - robotRadius, point.X + robotRadius]  == OBSTACLE_CELL ||
                    mapArray[point.Y - robotRadius, point.X] == OBSTACLE_CELL ||
                    mapArray[point.Y - robotRadius, point.X - robotRadius]  == OBSTACLE_CELL ||
                    mapArray[point.Y, point.X - robotRadius]  == OBSTACLE_CELL ||
                    mapArray[point.Y + robotRadius, point.X - robotRadius]  == OBSTACLE_CELL
                    ;
        }

        bool OutOfBound(Point p, int robotRadius)
        {
            return (p.X - robotRadius < 0 ||
                    p.Y - robotRadius < 0 ||
                    p.X + robotRadius >= Width ||
                    p.Y + robotRadius >= Height);
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
        Map _testMap = new Map("../../../../ai_map/mytest.csv");

        public Map GetMap(MapType type)
        {
            switch (type)
            {
                case MapType.Test:
                    return _testMap;
                default:
                    throw new MyException();
            }
        }
    }
}
