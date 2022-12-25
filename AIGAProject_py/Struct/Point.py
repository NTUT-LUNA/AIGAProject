from enum.Direction import Direction

class Point:
    def __init__(self, x1: int, y1: int) -> None:
        self._X = x1
        self._Y = y1

    @property
    def X(self) -> int:
        return self._X
 
    @password.setter
    def X(self, value: int) -> None:
        self._X = value

    @property
    def Y(self) -> int:
        return self._Y
 
    @password.setter
    def Y(self, value: int) -> None:
        self._Y = value

    def copy(self) -> Point:
        return Point(X, Y)

    def next_location(self, direction: Direction):
        next_point = self.copy()
        if direction == Direction.N:
            next_point.Y++
        elif direction == Direction.NE:
            next_pointY++
            next_pointX++
        elif direction == Direction.E:
            next_pointX++
            break;
        elif direction == Direction.SE:
            next_pointY--
            next_pointX++
        elif direction == Direction.S:
            next_pointY--
            break;
        elif direction == Direction.SW:
            next_pointY--
            next_pointX--
        elif direction == Direction.W:
            next_pointX--
            break;
        elif direction == Direction.NW:
            next_pointY++
            next_pointX--
        return next_point
 