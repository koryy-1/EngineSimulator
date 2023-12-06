using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineSimulator.Models;

namespace EngineSimulator.EngineConfigs
{
    public class DefaultEngineConfig : IEngineConfig
    {
        public double Inertia { get; set; } = 10;
        public List<double> TorqueValues { get; set; } = new List<double> { 20, 75, 100, 105, 75, 0 };
        public List<double> SpeedValues { get; set; } = new List<double> { 0, 75, 150, 200, 250, 300 };
        public double OverheatingTemperature { get; set; } = 110;
        public double heatRateM { get; set; } = 0.01; // Hm
        public double heatRateV { get; set; } = 0.0001; // Hv
        public double coolingRateCoef { get; set; } = 0.1; // C
    }
}
