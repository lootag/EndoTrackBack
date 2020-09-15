using System;
using Entities;
using Persistence;
using Persistence.Repositories.Implementations;
using Xunit;
using System.Linq;
using Entities.Enums;

namespace UnitTests
{
    [Collection("Database")]
    public class MachineRepositoryTests : BaseTest
    {
        [Fact]
        public void Should_Machine_Fields_Be_Retrieved_Correctly()
        {
            //Arrange
            long? customerId;
            
            var customerDto = new Persistence.ORMEntities.Customer
                            {
                                CustomerId = null,
                                Name = "Luigi"
                            };            
            var factory = new ContextFactory(this.retriever.GetConnectionString());
            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(customerDto);
                        context.SaveChanges();
                        customerId = customerDto.CustomerId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var machineDto = new Persistence.ORMEntities.Machine
            {
                MachineId = null,
                MachineNumber = "Machine_Number",
                OnlineFrom = DateTime.Now,
                SerialNumber = 123456789,
                MachineType = MachineType.EWD440PT,
                MachineIdByCustomer = 21,
                CustomerId = customerId.Value

            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(machineDto);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            //Act
            var machineRepository = new MachineRepository(factory);
            var machines = machineRepository.GetAll();
            var machineToAssert = machines[0];

            //Assert
            Assert.NotNull(machines);
            Assert.True(machines.Count == 1);
            Assert.True(machineToAssert.Id == machineDto.MachineId);
            Assert.True(machineToAssert.MachineIdByCustomer == machineDto.MachineIdByCustomer);
            Assert.True(machineToAssert.MachineNumber == machineDto.MachineNumber);
            Assert.True(machineToAssert.SerialNumber == machineDto.SerialNumber);
            Assert.True(machineToAssert.OnlineFrom.ToString() == machineDto.OnlineFrom.ToString());
            Assert.True(machineToAssert.CustomerId == machineDto.CustomerId);
            Assert.True(machineToAssert.Customer.Id == customerDto.CustomerId);
            Assert.True(machineToAssert.Customer.Name == customerDto.Name);
              


        }

        [Fact]
        public void Should_Two_Machines_Be_Retrieved()
        {
            //Arrange
            long? customerId;
            
            var customerDto = new Persistence.ORMEntities.Customer
                            {
                                CustomerId = null,
                                Name = "Luigi"
                            };            
            var factory = new ContextFactory(this.retriever.GetConnectionString());
            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(customerDto);
                        context.SaveChanges();
                        customerId = customerDto.CustomerId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var machineDto = new Persistence.ORMEntities.Machine
            {
                MachineId = null,
                MachineNumber = "Machine_Number",
                OnlineFrom = DateTime.Now,
                SerialNumber = 123456789,
                MachineType = MachineType.EWD440PT,
                MachineIdByCustomer = 21,
                CustomerId = customerId.Value

            };

            var machineDto2 = new Persistence.ORMEntities.Machine
            {
                MachineId = null,
                MachineNumber = "Machine_Number",
                OnlineFrom = DateTime.Now,
                SerialNumber = 123456780,
                MachineType = MachineType.EWD440PT,
                MachineIdByCustomer = 22,
                CustomerId = customerId.Value

            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(machineDto);
                        context.Add(machineDto2);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            //Act
            var machineRepository = new MachineRepository(factory);
            var machines = machineRepository.GetAll();

            //Assert
            Assert.NotNull(machines);
            Assert.True(machines.Count == 2);
        }
    }
}