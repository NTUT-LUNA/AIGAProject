from dataclasses import dataclass
from Point import Point

@dataclass
class RobotResult:
    index: int
    location: Point
