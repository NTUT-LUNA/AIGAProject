from enum import Enum, auto

class PointType(Enum):
    Empty= auto()
    Obstacle = auto()
    Start= auto()
    Goal = auto()
