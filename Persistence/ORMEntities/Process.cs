using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Persistence.ORMEntities
{
    [Table("Processes")]
    public class Process
    {
        [Key]
        [Column("Id")]
        public long? ProcessId { get; set; }
        [Column("Process_Type")]
        public ProcessType ProcessType { get; set; }
        [Column("Process_Time_Start")]
        public DateTime ProcessTimeStart { get; set; }
        [Column("Process_Time_End")]
        public DateTime ProcessTimeEnd { get; set; }
        [Column("Water_Temp")]
        public double WaterTemp { get; set; }
        [Column("Pump_10")]
        public bool Pump10 { get; set; }
        [Column("Pump_5")]
        public bool Pump5 { get; set; }
        [Column("Drain_Sensor")]
        public bool DrainSensor { get; set; }
        [Column("Water_Level_Ml")]
        public double WaterLevelMl { get; set; }    
        [Column("Machine_Id")]
        public long MachineId { get; set; }
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; }


    }


}