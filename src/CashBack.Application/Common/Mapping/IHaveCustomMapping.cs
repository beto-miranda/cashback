using AutoMapper;

namespace CashBack.Application.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
