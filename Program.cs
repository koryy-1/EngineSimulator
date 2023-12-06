using EngineSimulator.EngineConfigs;
using EngineSimulator.Models;
using EngineSimulator.Tests;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EngineSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test engine v2.0");

            // Ввод исходных данных
            double ambientTemperature = GetAmbientTemperatureFromUser();

            // шаг времени (в секундах), с помощью него можно контролировать погрешность
            double timeStep = GetTimeStepFromUser();
            //double timeStep = 0.1;

            // Запуск тестов
            var choice = GetChoiceFromUser();
            while (choice != "exit")
            {
                var engineConfig = new DefaultEngineConfig();
                Engine engine = new Engine(engineConfig, ambientTemperature);

                if (int.Parse(choice) == 1)
                {
                    ITest heatingTest = new HeatingTest();
                    var LastEngineMetrics = heatingTest.RunTest(engine, timeStep);
                    Console.WriteLine($"Time to overheat: {LastEngineMetrics?.Time} seconds");
                }
                else if (int.Parse(choice) == 2)
                {
                    ITest maxPowerTest = new MaxPowerTest();
                    var engineMetricsWithMaxPower = maxPowerTest.RunTest(engine, timeStep);
                    Console.WriteLine($"Max engine power: {engineMetricsWithMaxPower.EnginePower} kW");
                    Console.WriteLine($"crankshaft speed at max power: {engineMetricsWithMaxPower.Speed} rad/s");
                }

                choice = GetChoiceFromUser();
            }
        }

        private static double GetTimeStepFromUser()
        {
            Console.Write("Enter time step for control measurement error: ");
            return double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        }

        private static string GetChoiceFromUser()
        {
            Console.WriteLine("Choose the test:\n1 - run heating test\n2 - run maximum power test");
            Console.Write("> ");
            return Console.ReadLine();
        }

        private static double GetAmbientTemperatureFromUser()
        {
            Console.Write("Enter ambient temperature (Celsius): ");
            return double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        }
    }
}
