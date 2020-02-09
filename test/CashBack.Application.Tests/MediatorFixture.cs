using System.Reflection;
using AutoMapper;
using CashBack.Application.Common.Mapping;
using CashBack.Application.Pipelines;
using CashBack.Application.Revendedor.Commands.CriarRevendedor;
using CashBack.Domain.Repository;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CashBack.Application.Tests
{
    public class MediatorFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public IMediator Instance { get; private set; }
        public Mock<IOrderRepository> OrderRepositoryMock { get; private set; }
        public Mock<IResellerRepository> ResellerRepositoryMock { get; private set; }
        public MediatorFixture()
        {
            BuildMediator();
        }
        private void BuildMediator()
        {
            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());

            services.AddValidatorsFromAssemblyContaining<CreateResellerValidator>();

            services.AddAutoMapper(new Assembly[]
            {
                typeof(AutoMapperProfile).GetTypeInfo().Assembly
            });
            
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddMediatR(typeof(CreateResellerCommand));
            
            OrderRepositoryMock = new Mock<IOrderRepository>();
            ResellerRepositoryMock = new Mock<IResellerRepository>();
            services.AddSingleton(OrderRepositoryMock.Object);
            services.AddSingleton(ResellerRepositoryMock.Object);

            ServiceProvider = services.BuildServiceProvider();
            Instance = ServiceProvider.GetRequiredService<IMediator>();
        }
    }
}
