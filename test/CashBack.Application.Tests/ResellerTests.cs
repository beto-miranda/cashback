using System.Collections.Generic;
using System.Threading.Tasks;
using CashBack.Application.Common.Exceptions;
using CashBack.Application.Revendedor.Commands.CriarRevendedor;
using CashBack.Domain.Entities;
using Moq;
using Xunit;

namespace CashBack.Application.Tests
{
    public class ResellerTests : IClassFixture<MediatorFixture>
    {
        private MediatorFixture fixture;

        public ResellerTests(MediatorFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateReseller_InvalidData_BusinessValidationException(CreateResellerCommand command)
        {
            // Assert
            await Assert.ThrowsAsync<BusinessValidationException>(async() => await fixture.Instance.Send(command));
        }

        [Fact]
        public async Task CreateReseller_ValidData_Success()
        {
            // Arrange
            var command = new CreateResellerCommand 
            {
                Name = "Fulano da silva",
                CPF = "84567632761",
                Email = "fulano@teste.com",
                Password = "P@ssword1"
            };

            // Act
            await fixture.Instance.Send(command);

            // ßAssert
            fixture.ResellerRepositoryMock.Verify(m => m.CreateAsync(It.IsAny<Reseller>()), Times.Once());
        }

        public static IEnumerable<object[]> InvalidData => new List<object[]>
        {
            new object[] { new CreateResellerCommand {} },
            new object[] { new CreateResellerCommand { Name = "Teste"}},
            new object[] { new CreateResellerCommand { Name = "Teste", CPF = "123"}},
            new object[] { new CreateResellerCommand { Name = "Teste", CPF = "123", Email = "teste" }},
            new object[] { new CreateResellerCommand { Name = "Teste", CPF = "123", Email = "teste@teste.com" }},
            //new object[] { new CreateResellerCommand { Name = "Teste", CPF = "123", Email = "teste@teste.com", Password = "password" }},
        };
    }
}
