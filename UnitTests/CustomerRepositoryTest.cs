using System;
using Entities;
using Persistence;
using Persistence.Repositories.Implementations;
using Xunit;
using System.Linq;


namespace UnitTests
{
    [Collection("Database")]
    public class CustomerRepositoryTest : BaseTest
    {
        [Fact]
        public void Should_Customer_Fields_Be_Retrieved_Correctly()
        {
            //Arrange
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
            var customerRepository = new CustomerRepository(factory);
            var customers = customerRepository.GetAll();
            var customerToAssert = customers[0];

            //Assert
            Assert.NotNull(customers);
            Assert.True(customers.Count == 1);
            Assert.True(customerToAssert.Id == customerDto.CustomerId);
            Assert.True(customerToAssert.Name == customerDto.Name);
        }

        [Fact]
        public void Should_Two_Customers_Be_Retrieved()
        {
            //Arrange
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

            using(var context = factory.CreateContext())
            {
                using(var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Add(customerDto2);
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
            var customerRepository = new CustomerRepository(factory);
            var customers = customerRepository.GetAll();
            

            //Assert
            Assert.NotNull(customers);
            Assert.True(customers.Count == 2);
        }
    }
}
