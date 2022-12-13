using System;

namespace AIGAProject.Model
{
    public class Model
    {
        Maps maps = new Maps();

        public void LoadMap(MapType type)
        {
            Simulation simulation = new Simulation(maps.GetMap(type));
            simulation.Start();
        }

        public void Draw(IGraphics graphics)
        {

        }
    }
}
