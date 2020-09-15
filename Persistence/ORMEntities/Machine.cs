using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Persistence.ORMEntities
{
    [Table("Machines")]
    public class Machine
    {
        [Key]
        [Column("Id")]
        public long? MachineId { get; set; }
        [Column("Machine_Number")]
        public string MachineNumber { get; set; }
        [Column("Online_From")]
        public DateTime OnlineFrom { get; set; }
        [Column("Serial_Number")]
        public long SerialNumber { get; set; }
        [Column("Machine_Type")]
        public MachineType MachineType { get; set; }
        [Column("Machine_Id_By_Customer")]
        public int MachineIdByCustomer { get; set; }
        [Column("Customer_Id")]
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}