using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashBack.Application.Common.Exceptions;
using CashBack.Application.Orders.Commands.CreateOrder;
using CashBack.Application.Orders.Commands.DeleteOrder;
using CashBack.Application.Orders.Commands.EditOrder;
using CashBack.Domain.Entities;
using Moq;
using Xunit;

namespace CashBack.Application.Tests
{
    public class OrdersTests : IClassFixture<MediatorFixture>
    {
        private MediatorFixture fixture;

        public OrdersTests(MediatorFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateOrder_InvalidData_BusinessValidationException(CreateOrderCommand command)
        {
            // Assert
            await Assert.ThrowsAsync<BusinessValidationException>(async() => await fixture.Instance.Send(command));
        }

        [Fact]
        public async Task CreateOrder_ValidData_Success()
        {
            // Arrange
            var command = new CreateOrderCommand 
            {
                Id = Guid.NewGuid(),
                Amount = 1000,
                ResellerCpf = "84567632761"
            };
            var reseller = new Reseller
            {
                CPF = command.ResellerCpf
            };
            fixture.ResellerRepositoryMock.Setup(foo => foo.GetByCpfAsync(command.ResellerCpf)).ReturnsAsync(reseller);

            // Act
            await fixture.Instance.Send(command);

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Order>()), Times.Once());
        }

        [Fact]
        public async Task EditOrder_NonExistentOrder_BusinessValidationException()
        {
            // Arrange
            var command = new EditOrderCommand 
            {
                OrderId = Guid.NewGuid(),
                Amount = 1000
            };
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(command.OrderId)).ReturnsAsync((Order)null);

            // Act
            await Assert.ThrowsAsync<NotFoundException>(async() => await fixture.Instance.Send(command));

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Order>()), Times.Never());
        }

        [Fact]
        public async Task EditOrder_OrderAproved_BusinessValidationException()
        {
            // Arrange
            var command = new EditOrderCommand 
            {
                OrderId = Guid.NewGuid(),
                Amount = 1000
            };
            var order = new Order
            {
                Id = command.OrderId,
                Status = OrderStatus.Approved
            };
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(order.Id)).ReturnsAsync(order);

            // Act
            await Assert.ThrowsAsync<BusinessValidationException>(async() => await fixture.Instance.Send(command));

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Order>()), Times.Never());
        }

        [Fact]
        public async Task EditOrder_ValidData_Success()
        {
            // Arrange
            var command = new EditOrderCommand 
            {
                OrderId = Guid.NewGuid(),
                Amount = 1000
            };
            var order = new Order
            {
                Id = command.OrderId,
                Status = OrderStatus.Validation
            };
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(order.Id)).ReturnsAsync(order);

            // Act
            await fixture.Instance.Send(command);

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.UpdateAsync(It.IsAny<Order>()), Times.Once());
        }

        [Fact]
        public async Task DeleteOrder_OrderAproved_BusinessValidationException()
        {
            // Arrange
            var command = new DeleteOrderCommand(Guid.NewGuid());
            var order = new Order
            {
                Id = command.OrderId,
                Status = OrderStatus.Approved
            };
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(order.Id)).ReturnsAsync(order);

            // Act
            await Assert.ThrowsAsync<BusinessValidationException>(async() => await fixture.Instance.Send(command));

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.DeleteAsync(order.Id), Times.Never());
        }

        [Fact]
        public async Task DeleteOrder_NonExistentOrder_NotFoundException()
        {
            // Arrange
            var command = new DeleteOrderCommand(Guid.NewGuid());
         
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(command.OrderId)).ReturnsAsync((Order)null);

            // Act
            await Assert.ThrowsAsync<NotFoundException>(async() => await fixture.Instance.Send(command));

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.DeleteAsync(command.OrderId), Times.Never());
        }

        [Fact]
        public async Task DeleteOrder_ValidData_Success()
        {
            // Arrange
            var command = new DeleteOrderCommand(Guid.NewGuid());
            var order = new Order
            {
                Id = command.OrderId,
                Status = OrderStatus.Validation
            };
            fixture.OrderRepositoryMock.Setup(foo => foo.GetAsync(order.Id)).ReturnsAsync(order);

            // Act
            await fixture.Instance.Send(command);

            // Assert
            fixture.OrderRepositoryMock.Verify(m => m.DeleteAsync(order.Id), Times.Once());
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[] { new CreateOrderCommand {} },
            new object[] { new CreateOrderCommand { Id = Guid.NewGuid()}},
            new object[] { new CreateOrderCommand { Id = Guid.NewGuid(), Amount = -100}},
            new object[] { new CreateOrderCommand { Id = Guid.NewGuid(), Amount = 0}},
            //new object[] { new CreateOrderCommand { Id = Guid.NewGuid(), Amount = 100, ResellerCpf = "123456"}},
        };
    }
}
