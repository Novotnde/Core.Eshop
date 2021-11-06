using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Core.Utils
{
    public abstract class MapperProfile : Profile
    {
        public IMappingExpression<TSource, TDestination> CreateValidMap<TSource, TDestination>()
            where TDestination : notnull
        {
            var result = CreateMap<TSource, TDestination>()
                .AfterMap((_, response) =>
                {
                    var validationContext = new ValidationContext(response, null, null);
                    Validator.ValidateObject(response, validationContext, true);
                });

            return result;
        }
    }
}
