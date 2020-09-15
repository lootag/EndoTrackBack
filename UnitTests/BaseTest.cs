using System;
using System.Linq;
using Persistence;
using UnitTests.Helpers;

namespace UnitTests
{
    public class BaseTest
    {
        public ConfigurationRetriever retriever = new ConfigurationRetriever();
        public BaseTest()
        {
            var connectionString = this.retriever.GetConnectionString();
            var factory = new ContextFactory(connectionString);
                                         
            using(var context = factory.CreateContext())
            {
                var processes = context.Processes
                                    .ToList();
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach(var process in processes)
                        {
                            context.Remove(process);
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            using(var context = factory.CreateContext())
            {
                var machines = context.Machines
                                    .ToList();
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach(var machine in machines)
                        {
                            context.Remove(machine);
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            using(var context = factory.CreateContext())
            {
                var customers = context.Customers
                                    .ToList();
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach(var customer in customers)
                        {
                            context.Remove(customer);
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}