using EngineSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineSimulator
{
    public class Engine
    {
        private double inertia;
        private List<double> torqueValues;
        private List<double> speedValues;
        private double heatRateM;
        private double heatRateV;
        private double coolingRateCoef;
        private double overheatingTemperature;

        private double torque;
        private double speed;
        // в зависимости от ускорения изменяются текущие значения крутящего момента и скорость коленвала двигателя
        // если acceleration = 0 то крутящий момент и скорость коленвала меняться не будут
        private double acceleration;
        private double engineTemperature;
        private double ambientTemperature;
        private double power;

        private double M1;
        private double M2;
        private double V1;
        private double V2;
        private int lineNum;

        public double OverheatingTemperature { get => overheatingTemperature; }

        public Engine(IEngineConfig engineConfig, double ambientTemperature)
        {
            inertia = engineConfig.Inertia;
            torqueValues = engineConfig.TorqueValues;
            speedValues = engineConfig.SpeedValues;
            overheatingTemperature = engineConfig.OverheatingTemperature;
            heatRateM = engineConfig.heatRateM;
            heatRateV = engineConfig.heatRateV;
            coolingRateCoef = engineConfig.coolingRateCoef;

            torque = torqueValues[0];
            speed = speedValues[0];
            acceleration = 0;
            engineTemperature = ambientTemperature;
            power = 0;

            M1 = torqueValues[0];
            M2 = torqueValues[1];
            V1 = speedValues[0];
            V2 = speedValues[1];
        }

        public void CheckSpeedWithinBounds()
        {
            if (speed >= speedValues[lineNum + 1])
            {
                StepToNextTorqueAndSpeedValues();
            }
            else if (speed < speedValues[lineNum])
            {
                StepToPrevTorqueAndSpeedValues();
            }
        }

        public void StepToNextTorqueAndSpeedValues()
        {
            if (lineNum < 5)
            {
                lineNum++;
                M1 = torqueValues[lineNum];
                M2 = torqueValues[lineNum + 1];
                V1 = speedValues[lineNum];
                V2 = speedValues[lineNum + 1];
            }
        }

        public void StepToPrevTorqueAndSpeedValues()
        {
            if (lineNum > 0)
            {
                lineNum--;
                M1 = torqueValues[lineNum];
                M2 = torqueValues[lineNum + 1];
                V1 = speedValues[lineNum];
                V2 = speedValues[lineNum + 1];
            }
        }

        public EngineMetrics SimulateOneWorkCycle(double timeStep)
        {
            speed += acceleration * timeStep;

            // Вычисляем значение крутящего момента с использованием интерполяции
            torque = M1 + ((speed - V1) / (V2 - V1)) * (M2 - M1);

            acceleration = torque / inertia;

            power = torque * speed / 1000;

            double heatingRate = torque * heatRateM + Math.Pow(speed, 2) * heatRateV;
            double coolingRate = coolingRateCoef * (ambientTemperature - engineTemperature);

            double temperatureChange = (heatingRate - coolingRate) * timeStep;
            engineTemperature += temperatureChange;

            EngineMetrics engineMetrics = new EngineMetrics
            {
                Speed = speed,
                Torque = torque,
                Acceleration = acceleration,
                EngineTemperature = engineTemperature,
                EnginePower = power,
            };

            return engineMetrics;
        }
    }
}
