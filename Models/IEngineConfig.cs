using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulator.Models
{
    public interface IEngineConfig
    {
        public double Inertia { get; set; }
        public List<double> TorqueValues { get; set; }
        public List<double> SpeedValues { get; set; }
        public double OverheatingTemperature { get; set; }
        public double heatRateM { get; set; } // Hm
        public double heatRateV { get; set; } // Hv
        public double coolingRateCoef { get; set; } // C
    }
}
