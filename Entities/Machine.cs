using System;
using Entities.Enums;

namespace Entities
{
    public class Machine
    {

        public Machine(long? id,
                        string machineNumber,
                        DateTime onlineFrom,
                        int machineIdByCustomer,
                        long customerId,
                        MachineType machineType,
                        long serialNumber, Customer customer)
        {
            this.Id = id;
            this.MachineNumber = machineNumber;
            this.OnlineFrom = onlineFrom;
            this.MachineIdByCustomer = machineIdByCustomer;
            this.CustomerId = customerId;
            this.MachineType = machineType;
            SerialNumber = serialNumber;
            Customer = customer;
        }
        public long? Id { get; }
        public string MachineNumber { get; }
        public DateTime OnlineFrom { get; }
        public int MachineIdByCustomer { get; }
        public long SerialNumber { get; }
        public long CustomerId { get; }
        public Customer Customer { get; }
        public MachineType MachineType { get; }
    }
}