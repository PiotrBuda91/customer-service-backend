using CustomerApi.Domain.DTOs;
using CustomerApi.Infrastructure.Data;
using CustomerApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CustomerApi.IntegrationTests;

[TestFixture]
public class CustomerRepositoryTests
{
    private DbContextOptions<CustomerContext> _options;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<CustomerContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Test]
    public async Task AddCustomer_ValidCustomer_SavesToDatabase()
    {
        // Arrange
        using (var context = new CustomerContext(_options))
        {
            var repository = new CustomerRepository(context);
            var customer = new Customer { Id = Guid.NewGuid().ToString(), Name = "Piotr Buda", Email = "piotr.buda@blabla.com" };

            // Act
            await repository.AddCustomer(customer);

            // Assert
            var savedCustomer = context.Customers.FirstOrDefault();
            Assert.IsNotNull(savedCustomer);
            Assert.AreEqual("Piotr Buda", savedCustomer.Name);
            Assert.AreEqual("piotr.buda@blabla.com", savedCustomer.Email);
        }
    }
}