using EngineSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulator.Tests
{
    public class MaxPowerTest : ITest
    {
        private static double accelerationThreshold = 0.005;
        public EngineMetrics RunTest(Engine engine, double timeStep)
        {
            EngineMetrics engineMetricsMaxPower = new EngineMetrics();
            for (double time = 0; time < 3600; time += timeStep)
            {
                engine.CheckSpeedWithinBounds();

                EngineMetrics engineMetrics = engine.SimulateOneWorkCycle(timeStep);

                if (engineMetricsMaxPower.EnginePower < engineMetrics.EnginePower)
                    engineMetricsMaxPower = engineMetrics;

                if (engineMetrics.Acceleration < accelerationThreshold)
                    break;
            }

            return engineMetricsMaxPower;
        }
    }
}
