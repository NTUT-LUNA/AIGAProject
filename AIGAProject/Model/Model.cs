using System;

namespace AIGAProject.Model
{
    public class Model
    {
        Maps maps = new Maps();

        public void LoadTestMap()
        {
            Simulation simulation = new Simulation(maps.TestMap);
            simulation.StartSimulation();
        }
    }
}
