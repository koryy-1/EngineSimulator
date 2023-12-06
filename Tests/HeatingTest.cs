using EngineSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulator.Tests
{
    public class HeatingTest : ITest
    {
        public EngineMetrics RunTest(Engine engine, double timeStep)
        {
            for (double time = 0; time < 3600; time += timeStep)
            {
                engine.CheckSpeedWithinBounds();

                EngineMetrics engineMetrics = engine.SimulateOneWorkCycle(timeStep);

                if (engineMetrics.EngineTemperature >= engine.OverheatingTemperature)
                {
                    engineMetrics.Time = time;
                    return engineMetrics;
                }
            }

            return null;
        }
    }
}
