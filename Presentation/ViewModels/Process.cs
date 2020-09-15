using System;

namespace Presentation.ViewModels
{
    public class Process
    {
        public long? Id { get; set; }
        public int? ProcessType { get; set; }
        public DateTime? ProcessTimeStart { get; set; }
        public DateTime? ProcessTimeEnd { get; set; }
        public double? WaterTemp { get; set; }
        public bool? Pump10 { get; set; }
        public bool? Pump5 { get; set; }
        public bool? DrainSensor { get; set; }
        public double? WaterLevelMl { get; set; }
        public long? MachineId { get; set; }
        public Machine Machine { get; set; }
    }
}