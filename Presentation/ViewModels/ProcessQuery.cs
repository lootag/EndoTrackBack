namespace Presentation.ViewModels
{
    public class ProcessQuery
    {
        public double? WaterTempMin { get; set; }
        public double? WaterTempMax { get; set; }
        public bool? Pump10 { get; set; }
        public bool? Pump5 { get; set; }
        public bool? DrainSensor { get; set; }
        public double? WaterLevelMlMin { get; set; }
        public double? WaterLevelMlMax { get; set; }
        public long[] MachineIds { get; set; }
        public long[] CustomerIds { get; set; }
    }
}