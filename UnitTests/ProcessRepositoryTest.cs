using System;
using Persistence;
using Xunit;
using Entities.Enums;
using Persistence.Repositories.Implementations;
using Entities.Query;

namespace UnitTests
{
    [Collection("Database")]
    public class ProcessRepositoryTest : BaseTest
    {

        [Fact]
        public void Should_Process_Fields_Be_Retrieved_Correctly()
        {
            //Arrange
            long? customerId;
            long? machineId;
            long? processId;
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
                        machineId = machineDto.MachineId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var processDto = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 20,
                Pump10 = true,
                Pump5 = false,
                DrainSensor = true,
                WaterLevelMl = 300,
                MachineId = machineId.Value
            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(processDto);
                        context.SaveChanges();
                        processId = processDto.ProcessId;
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
            var processRepository = new ProcessRepository(factory);
            var retrievedProcesses = processRepository.GetAll();
            
            //Assert
            Assert.NotNull(retrievedProcesses);
            Assert.True(retrievedProcesses.Count == 1);
            Assert.True(retrievedProcesses[0].Id == processId);
            Assert.True(retrievedProcesses[0].MachineId == machineId);
            Assert.True(retrievedProcesses[0].ProcessTimeEnd.ToString() == processDto.ProcessTimeEnd.ToString());
            Assert.True(retrievedProcesses[0].ProcessTimeStart.ToString() == processDto.ProcessTimeStart.ToString());
            Assert.True(retrievedProcesses[0].Pump10 == processDto.Pump10);
            Assert.True(retrievedProcesses[0].Pump5 == processDto.Pump5);
            Assert.True(retrievedProcesses[0].WaterTemp == processDto.WaterTemp);
            Assert.True(retrievedProcesses[0].WaterLevelMl == processDto.WaterLevelMl);
            Assert.True(retrievedProcesses[0].DrainSensor == processDto.DrainSensor);
            Assert.True(retrievedProcesses[0].ProcessType == processDto.ProcessType);
            Assert.True(retrievedProcesses[0].Machine.Id == machineDto.MachineId);
            Assert.True(retrievedProcesses[0].Machine.MachineIdByCustomer == machineDto.MachineIdByCustomer);
            Assert.True(retrievedProcesses[0].Machine.MachineNumber == machineDto.MachineNumber);
            Assert.True(retrievedProcesses[0].Machine.MachineType == machineDto.MachineType);
            Assert.True(retrievedProcesses[0].Machine.OnlineFrom.ToString() == machineDto.OnlineFrom.ToString());
            Assert.True(retrievedProcesses[0].Machine.SerialNumber == machineDto.SerialNumber);
            Assert.True(retrievedProcesses[0].Machine.CustomerId == machineDto.CustomerId);
            Assert.True(retrievedProcesses[0].Machine.Customer.Id == customerId);
            Assert.True(retrievedProcesses[0].Machine.Customer.Name == customerDto.Name);
        }

        [Fact]
        public void Should_Two_Entities_Be_Retrieved()
        {
            //Arrange
            long? customerId;
            long? machineId;
            long? processId;
            long? processId2;
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
                        machineId = machineDto.MachineId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var processDto = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 20,
                Pump10 = true,
                Pump5 = false,
                DrainSensor = true,
                WaterLevelMl = 300,
                MachineId = machineId.Value
            };

            var processDto2 = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 25,
                Pump10 = false,
                Pump5 = true,
                DrainSensor = false,
                WaterLevelMl = 380,
                MachineId = machineId.Value
            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(processDto);
                        context.Add(processDto2);
                        context.SaveChanges();
                        transaction.Commit();
                        processId = processDto.ProcessId;
                        processId2 = processDto2.ProcessId;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            //Act
            var processRepository = new ProcessRepository(factory);
            var retrievedProcesses = processRepository.GetAll();

            //Assert
            Assert.NotNull(retrievedProcesses);
            Assert.True(retrievedProcesses.Count == 2);
        }

        [Fact]
        public void Should_Correct_Process_Be_Retrieved_By_Id()
        {
            //Arrange
            long? customerId;
            long? machineId;
            long? processId;
            long? processId2;
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
                        machineId = machineDto.MachineId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var processDto = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 20,
                Pump10 = true,
                Pump5 = false,
                DrainSensor = true,
                WaterLevelMl = 300,
                MachineId = machineId.Value
            };

            var processDto2 = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 25,
                Pump10 = false,
                Pump5 = true,
                DrainSensor = false,
                WaterLevelMl = 380,
                MachineId = machineId.Value
            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(processDto);
                        context.Add(processDto2);
                        context.SaveChanges();
                        processId = processDto.ProcessId;
                        processId2 = processDto2.ProcessId;
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
            var processRepository = new ProcessRepository(factory);
            var retrievedProcess = processRepository.Get(processDto2.ProcessId);
            
            //Assert
            Assert.NotNull(retrievedProcess);
            Assert.True(retrievedProcess.Id == processId2);
            Assert.True(retrievedProcess.MachineId == machineId);
            Assert.True(retrievedProcess.ProcessTimeEnd.ToString() == processDto2.ProcessTimeEnd.ToString());
            Assert.True(retrievedProcess.ProcessTimeStart.ToString() == processDto2.ProcessTimeStart.ToString());
            Assert.True(retrievedProcess.Pump10 == processDto2.Pump10);
            Assert.True(retrievedProcess.Pump5 == processDto2.Pump5);
            Assert.True(retrievedProcess.WaterTemp == processDto2.WaterTemp);
            Assert.True(retrievedProcess.WaterLevelMl == processDto2.WaterLevelMl);
            Assert.True(retrievedProcess.DrainSensor == processDto2.DrainSensor);
            Assert.True(retrievedProcess.ProcessType == processDto2.ProcessType);
            Assert.True(retrievedProcess.Machine.Id == machineDto.MachineId);
            Assert.True(retrievedProcess.Machine.MachineIdByCustomer == machineDto.MachineIdByCustomer);
            Assert.True(retrievedProcess.Machine.MachineNumber == machineDto.MachineNumber);
            Assert.True(retrievedProcess.Machine.MachineType == machineDto.MachineType);
            Assert.True(retrievedProcess.Machine.OnlineFrom.ToString() == machineDto.OnlineFrom.ToString());
            Assert.True(retrievedProcess.Machine.SerialNumber == machineDto.SerialNumber);
            Assert.True(retrievedProcess.Machine.CustomerId == machineDto.CustomerId);
            Assert.True(retrievedProcess.Machine.Customer.Id == customerId);
            Assert.True(retrievedProcess.Machine.Customer.Name == customerDto.Name);


        }

        [Fact]
        public void Should_Process_Query_With_No_Null_Values_Succed()
        {
            //Arrange
            long? customerId;
            long? customerId2;
            long? machineId;
            long? machineId2;
            long? processId;
            long? processId2;
            var customerDto = new Persistence.ORMEntities.Customer
            {
                CustomerId = null,
                Name = "Luigi"
            };
            var customerDto2 = new Persistence.ORMEntities.Customer
            {
                CustomerId = null,
                Name = "Alex"
            };
            var factory = new ContextFactory(this.retriever.GetConnectionString());
            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(customerDto);
                        context.Add(customerDto2);
                        context.SaveChanges();
                        customerId = customerDto.CustomerId;
                        customerId2 = customerDto2.CustomerId;
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
                SerialNumber = 123456788,
                MachineType = MachineType.EWD440PT,
                MachineIdByCustomer = 22,
                CustomerId = customerId2.Value

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
                        machineId = machineDto.MachineId;
                        machineId2 = machineDto2.MachineId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var processDto = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 20,
                Pump10 = true,
                Pump5 = false,
                DrainSensor = true,
                WaterLevelMl = 300,
                MachineId = machineId.Value
            };

            var processDto2 = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 25,
                Pump10 = false,
                Pump5 = true,
                DrainSensor = false,
                WaterLevelMl = 380,
                MachineId = machineId2.Value
            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(processDto);
                        context.Add(processDto2);
                        context.SaveChanges();
                        processId = processDto.ProcessId;
                        processId2 = processDto2.ProcessId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            long[] machineIds = new long[] {machineId2.Value};
            long[] customerIds = new long[] {customerId2.Value};


            var processQuery = new ProcessQuery(22, 26, false, true, false, 360, 390, machineIds, customerIds);

            //Act 
            var processRepository = new ProcessRepository(factory);
            var retrievedProcesses = processRepository.GetByQueryValues(processQuery);

            //Assert
            Assert.NotNull(retrievedProcesses);
            Assert.True(retrievedProcesses.Count == 1);
            Assert.True(retrievedProcesses[0].Id == processId2);
            Assert.True(retrievedProcesses[0].MachineId == machineId2);
            Assert.True(retrievedProcesses[0].ProcessTimeEnd.ToString() == processDto2.ProcessTimeEnd.ToString());
            Assert.True(retrievedProcesses[0].ProcessTimeStart.ToString() == processDto2.ProcessTimeStart.ToString());
            Assert.True(retrievedProcesses[0].Pump10 == processDto2.Pump10);
            Assert.True(retrievedProcesses[0].Pump5 == processDto2.Pump5);
            Assert.True(retrievedProcesses[0].WaterTemp == processDto2.WaterTemp);
            Assert.True(retrievedProcesses[0].WaterLevelMl == processDto2.WaterLevelMl);
            Assert.True(retrievedProcesses[0].DrainSensor == processDto2.DrainSensor);
            Assert.True(retrievedProcesses[0].ProcessType == processDto2.ProcessType);
            Assert.True(retrievedProcesses[0].Machine.Id == machineDto2.MachineId);
            Assert.True(retrievedProcesses[0].Machine.MachineIdByCustomer == machineDto2.MachineIdByCustomer);
            Assert.True(retrievedProcesses[0].Machine.MachineNumber == machineDto2.MachineNumber);
            Assert.True(retrievedProcesses[0].Machine.MachineType == machineDto2.MachineType);
            Assert.True(retrievedProcesses[0].Machine.OnlineFrom.ToString() == machineDto2.OnlineFrom.ToString());
            Assert.True(retrievedProcesses[0].Machine.SerialNumber == machineDto2.SerialNumber);
            Assert.True(retrievedProcesses[0].Machine.CustomerId == machineDto2.CustomerId);
            Assert.True(retrievedProcesses[0].Machine.Customer.Id == customerId2);
            Assert.True(retrievedProcesses[0].Machine.Customer.Name == customerDto2.Name);
        }

        [Fact]
        public void Should_Process_Query_With_Null_Values_Succed()
        {
            //Arrange
            long? customerId;
            long? customerId2;
            long? machineId;
            long? machineId2;
            long? processId;
            long? processId2;
            var customerDto = new Persistence.ORMEntities.Customer
            {
                CustomerId = null,
                Name = "Luigi"
            };
            var customerDto2 = new Persistence.ORMEntities.Customer
            {
                CustomerId = null,
                Name = "Alex"
            };
            var factory = new ContextFactory(this.retriever.GetConnectionString());
            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(customerDto);
                        context.Add(customerDto2);
                        context.SaveChanges();
                        customerId = customerDto.CustomerId;
                        customerId2 = customerDto2.CustomerId;
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
                SerialNumber = 123456788,
                MachineType = MachineType.EWD440PT,
                MachineIdByCustomer = 22,
                CustomerId = customerId2.Value

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
                        machineId = machineDto.MachineId;
                        machineId2 = machineDto2.MachineId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            var processDto = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 20,
                Pump10 = true,
                Pump5 = false,
                DrainSensor = true,
                WaterLevelMl = 300,
                MachineId = machineId.Value
            };

            var processDto2 = new Persistence.ORMEntities.Process
            {
                ProcessId = null,
                ProcessType = ProcessType.WashDisWash,
                ProcessTimeStart = DateTime.Now,
                ProcessTimeEnd = DateTime.Now,
                WaterTemp = 25,
                Pump10 = false,
                Pump5 = true,
                DrainSensor = false,
                WaterLevelMl = 380,
                MachineId = machineId2.Value
            };

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(processDto);
                        context.Add(processDto2);
                        context.SaveChanges();
                        processId = processDto.ProcessId;
                        processId2 = processDto2.ProcessId;
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    

                }
            }

            long[] customerIds = new long[] {customerId2.Value};
            var processQuery = new ProcessQuery(null, null, null, null, null, null, null, null, customerIds);

            //Act 
            var processRepository = new ProcessRepository(factory);
            var retrievedProcesses = processRepository.GetByQueryValues(processQuery);

            //Assert
            Assert.NotNull(retrievedProcesses);
            Assert.True(retrievedProcesses.Count == 1);
            Assert.True(retrievedProcesses[0].Id == processId2);
            Assert.True(retrievedProcesses[0].MachineId == machineId2);
            Assert.True(retrievedProcesses[0].ProcessTimeEnd.ToString() == processDto2.ProcessTimeEnd.ToString());
            Assert.True(retrievedProcesses[0].ProcessTimeStart.ToString() == processDto2.ProcessTimeStart.ToString());
            Assert.True(retrievedProcesses[0].Pump10 == processDto2.Pump10);
            Assert.True(retrievedProcesses[0].Pump5 == processDto2.Pump5);
            Assert.True(retrievedProcesses[0].WaterTemp == processDto2.WaterTemp);
            Assert.True(retrievedProcesses[0].WaterLevelMl == processDto2.WaterLevelMl);
            Assert.True(retrievedProcesses[0].DrainSensor == processDto2.DrainSensor);
            Assert.True(retrievedProcesses[0].ProcessType == processDto2.ProcessType);
            Assert.True(retrievedProcesses[0].Machine.Id == machineDto2.MachineId);
            Assert.True(retrievedProcesses[0].Machine.MachineIdByCustomer == machineDto2.MachineIdByCustomer);
            Assert.True(retrievedProcesses[0].Machine.MachineNumber == machineDto2.MachineNumber);
            Assert.True(retrievedProcesses[0].Machine.MachineType == machineDto2.MachineType);
            Assert.True(retrievedProcesses[0].Machine.OnlineFrom.ToString() == machineDto2.OnlineFrom.ToString());
            Assert.True(retrievedProcesses[0].Machine.SerialNumber == machineDto2.SerialNumber);
            Assert.True(retrievedProcesses[0].Machine.CustomerId == machineDto2.CustomerId);
            Assert.True(retrievedProcesses[0].Machine.Customer.Id == customerId2);
            Assert.True(retrievedProcesses[0].Machine.Customer.Name == customerDto2.Name);
        }

    }
}