using CustomerApi.Application.Interfaces;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CustomerApi.Controllers;
using CustomerApi.Domain.DTOs;


namespace CustomerApi.Tests.Unit.Controllers;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _controller;
    private Mock<ICustomerRepository> _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<ICustomerRepository>();
        _controller = new CustomerController(_repository.Object);
    }

    [Test]
    public void GetCustomers_ReturnsListOfCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new() { Id = "cbe673c1-bb70-4050-891d-6792c9ca7ef0", Name = "Piotr Buda", Email = "piotr.buda@email.com" },
            new() { Id = "35b8e572-cc9c-4a48-a531-9d81a4187ba3", Name = "Peter Smith", Email = "peter.smith@email.com" }
        };
        _repository.Setup(r => r.GetCustomers()).ReturnsAsync(customers);

        // Act
        var result = _controller.GetCustomers();

        // Assert
        var okResult = result.Result;
        Assert.AreEqual(customers, okResult);
    }

    [Test]
    public void GetCustomer_ValidId_ReturnsCustomer()
    {
        // Arrange
        string customerId = "cbe673c1-bb70-4050-891d-6792c9ca7ef0";
        var customer = new Customer { Id = customerId, Name = "Piotr Buda", Email = "piotr.buda@email.com" };
        _repository.Setup(r => r.GetCustomerById(customerId)).ReturnsAsync(customer);

        // Act
        var result = _controller.GetCustomer(customerId);

        // Assert
        var okResult = result.Result;
        Assert.AreEqual(customer, okResult.Value);
    }

    [Test]
    public void UpdateCustomer_ValidCustomer_ReturnsNoContent()
    {
        // Arrange
        string customerId = "cbe673c1-bb70-4050-891d-6792c9ca7ef0";
        var customer = new Customer { Id = customerId, Name = "Piotr Buda", Email = "piotr.buda@email.com" };
        _repository.Setup(r => r.GetCustomerById(It.IsAny<string>())).ReturnsAsync(customer);

        // Act
        var result = _controller.UpdateCustomer(customerId, customer);

        // Assert
        _repository.Verify(r => r.UpdateCustomer(customer), Times.Once);
    }

    [Test]
    public void CreateCustomer_ValidCustomer_ReturnsCreatedResponse()
    {
        // Arrange
        var customer = new Customer { Name = "Piotr Buda", Email = "piotr.buda@email.com" };

        // Act
        var result = _controller.CreateCustomer(customer).Result;

        // Assert
        Assert.IsNotNull(result);
    }

    [Test]
    public void DeleteCustomer_ValidId_ReturnsNoContent()
    {
        // Arrange
        string customerId = "cbe673c1-bb70-4050-891d-6792c9ca7ef0";
        var customer = new Customer { Id = customerId, Name = "Piotr Buda", Email = "piotr.buda@email.com" };
        _repository.Setup(r => r.GetCustomerById(customerId)).ReturnsAsync(customer);

        // Act
        var result = _controller.DeleteCustomer(customerId);

        // Assert
        _repository.Verify(r => r.DeleteCustomer(customer), Times.Once);
    }
    
}
        