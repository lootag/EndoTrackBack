using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class Machine
    {
        public long? Id { get; set; }
        public string MachineNumber { get; set; }
        public DateTime? OnlineFrom { get; set; }
        public int? MachineIdByCustomer { get; set; }
        public long? SerialNumber { get; set; }
        public long? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? MachineType { get; set; }
    }
}