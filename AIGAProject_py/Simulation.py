from Maps import Map
from Robots import Robots
from enum.SelectMode import SelectMode

class Simulation:
    ROBOT_RADIUS: int = 1
    NUMBER_OF_ROBOTS: int = 10
    NUMBER_OF_STEPS: int = 10
    NUMBER_OF_GENERATIONS: int = 10
    MUTATION_RATE: float = 0.05
    
    def __init__(self, map_: Map) -> None:
        self.map = map_
        self.robots = Robots(map.StartPoint, self.ROBOT_RADIUS, self.NUMBER_OF_ROBOTS, self.NUMBER_OF_STEPS)

    def start(self) -> None:
        for generations in range(0, self.NUMBER_OF_GENERATIONS):
            # 機器人重設（步數、地點）
            self.robots.reset();
            # 機器人動
            self.robots.start_to_move();
            # 剔除後 50%
            self.robots.selection(SelectMode.Distance);
            # 交配
            self.robots.crossover();
            # 突變
            self.robots.mutation(self.MUTATION_RATE);