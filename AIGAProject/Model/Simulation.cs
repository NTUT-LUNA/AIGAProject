using System;
using System.Collections.Generic;
using System.Text;

namespace AIGAProject.Model
{
    class Simulation
    {
        Robots robots;
        Map map;
        const int NUMBER_OF_ROBOTS = 10;
        const int NUMBER_OF_STEPS = 10;
        const int NUMBER_OF_GENERATIONS = 10;
        const double MUTATION_RATE = 0.05;

        public Simulation(Map map)
        {
            this.map = map;
            robots = new Robots(map.StartPoint, NUMBER_OF_ROBOTS, NUMBER_OF_STEPS);
        }

        public void StartSimulation()
        {
            for (int generations = 0; generations < NUMBER_OF_GENERATIONS; generations++)
            {
                //機器人動
                robots.StartToMove(map);
                //剔除後 50%
                robots.Selection(map.GoalPoint);
                //交配
                robots.Crossover();
                //突變
                robots.Mutation(MUTATION_RATE);
            }
        }
    }
}
