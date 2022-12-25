from Maps import Maps
from Simulation import Simulation

class Model:
    def __init__(self) -> None:
        self.maps: Maps = Maps()

    def load_map(self, map_type: MapType) -> None:
        simulation: Simulation = Simulation(maps.get_map(map_type))
        simulation.start()
    
    def draw(self, graphics: IGraphics) -> None:
        pass