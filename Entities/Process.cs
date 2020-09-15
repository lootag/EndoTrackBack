using System;
using Entities.Enums;

namespace Entities
{
    public class Process
    {
        public Process(long? id, 
                       ProcessType processType, 
                       DateTime processTimeStart, 
                       DateTime processTimeEnd, 
                       long machineId, 
                       double waterTemp, 
                       bool pump10, 
                       bool pump5, 
                       bool drainSensor, 
                       double waterLevelMl, 
                       Machine machine)
        {
            this.Id = id;
            this.ProcessType = processType;
            this.ProcessTimeStart = processTimeStart;
            this.ProcessTimeEnd = processTimeEnd;
            this.MachineId = machineId;
            this.WaterTemp = waterTemp;
            this.Pump10 = pump10;
            this.Pump5 = pump5;
            this.DrainSensor = drainSensor;
            this.WaterLevelMl = waterLevelMl;
            this.Machine = machine;
        }
        public long? Id { get; }
        public ProcessType ProcessType { get; }
        public DateTime ProcessTimeStart { get; }
        public DateTime ProcessTimeEnd { get; }
        public double WaterTemp { get; }
        public bool Pump10 { get; }
        public bool Pump5 { get; }
        public bool DrainSensor { get; }
        public double WaterLevelMl { get; }
        public long MachineId { get; }
        public Machine Machine { get; }


    }
}