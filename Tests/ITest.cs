using EngineSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulator.Tests
{
    public interface ITest
    {
        EngineMetrics RunTest(Engine engine, double timeStep);
    }
}
