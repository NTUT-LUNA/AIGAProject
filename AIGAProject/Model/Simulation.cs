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

        public Simulation(Map map)
        {
            this.map = map;
            robots = new Robots(map.StartPoint, NUMBER_OF_ROBOTS, NUMBER_OF_STEPS);
        }

        public void StartSimulation()
        {
            for (int i = 0; i < NUMBER_OF_GENERATIONS; i++)
            {
                //機器人動
                robots.StartToMove(map);
                //evaluate() 評估結果
                robots.Selection(map.GoalPoint);
                robots.Crossover();
                robots.Mutation();
            }
        }
    }
}
