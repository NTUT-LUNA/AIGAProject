from typing import List
from Struct.Point import Point
from MyException import MyException
from enum.MapType import MapType

class Map:
    EMPTY_CELL: int = 0
    OBSTACLE_CELL: int = 1
    START_CELL: int = 2
    GOAL_CELL: int = 3

    def __init__(self, path: str) -> None:
        _height: int = 0
        _width: int = 0
        _start_point: Point = None
        _goal_point: Point = None
        map_array: List[int] = None
        
        with open(path, "r", encoding="utf-8") as file:
            for j, line in file:
                line = file.readline()
                values = line.split(",")

                height: int = len(values)
                width: int = len(values)
                for i in range(len(values)):
                    map_array = [[] * width] * height
                    int_value = int(value)
                    map_array[j][i] = int_value
                    if int_value == self.START_CELL:
                        self._start_point = Point(i, j)
                    elif int_value == self.GOAL_CELL:
                        self._goal_point = Point(i, j)
                

    @property
    def Height(self) -> int:
        return self._height
        
    @property
    def Width(self) -> int:
        return self._width
        
    @property
    def StartPoint(self) -> Point:
        return self._start_point
        
    @property
    def GoalPoint(self) -> Point:
        return self._goal_point

    def out_of_bound(self, p: Point, robot_radius: int) -> bool:
        return (
            p.X - robot_radius < 0 
            or p.Y - robot_radius < 0 
            or p.X + robot_radius >= self.Width 
            or p.Y + robot_radius >= self.Height
        )

    def is_obstacle(self, point: Point, robot_radius: int):
        return map_array[point.Y + robot_radius, point.X] == OBSTACLE_CELL or
            map_array[point.Y + robot_radius, point.X + robot_radius]  == OBSTACLE_CELL or
            map_array[point.Y, point.X + robot_radius]  == OBSTACLE_CELL or
            map_array[point.Y - robot_radius, point.X + robot_radius]  == OBSTACLE_CELL or
            map_array[point.Y - robot_radius, point.X] == OBSTACLE_CELL or
            map_array[point.Y - robot_radius, point.X - robot_radius]  == OBSTACLE_CELL or
            map_array[point.Y, point.X - robot_radius]  == OBSTACLE_CELL or
            map_array[point.Y + robot_radius, point.X - robot_radius]  == OBSTACLE_CELL

    def location_vaild(self, next_point: Point, robot_radius: int) -> bool:
        out_of_bound_check = out_of_bound(nextPoint, robot_radius)
        is_obstacle_check = is_obstacle(nextPoint, robot_radius)
        return not out_of_bound_check and not is_obstacle_check


class Maps:
    def __init__(self) -> None:
        self.test_map = Map("../../../../ai_map/mytest.csv")

    def get_map(self, map_type: MapType) -> Map:
        if map_type == MapType.Test:
            return self.test_map
        raise MyException()

