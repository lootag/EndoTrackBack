using System;
using Entities.Enums;

namespace Entities.Query
{
    public class ProcessQuery
    {
        public ProcessQuery(double? waterTempMin, 
                            double? waterTempMax, 
                            bool? pump10, 
                            bool? pump5, 
                            bool? drainSensor, 
                            double? waterLevelMlMin, 
                            double? waterLevelMlMax, 
                            long[] machineIds, 
                            long[] customerIds)
        {
            WaterTempMin = waterTempMin;
            WaterTempMax = waterTempMax;
            Pump10 = pump10;
            Pump5 = pump5;
            DrainSensor = drainSensor;
            WaterLevelMlMin = waterLevelMlMin;
            WaterLevelMlMax = waterLevelMlMax;
            MachineIds = machineIds;
            CustomerIds = customerIds;
        }

        public double? WaterTempMin { get; }
        public double? WaterTempMax { get; }
        public bool? Pump10 { get; }
        public bool? Pump5 { get; }
        public bool? DrainSensor { get; }
        public double? WaterLevelMlMin { get; }
        public double? WaterLevelMlMax { get; set; }
        public long[] MachineIds { get; }
        public long[] CustomerIds { get; }


    }
}