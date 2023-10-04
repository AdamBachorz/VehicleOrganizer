using AutoMapper;

namespace VehicleOrganizer.Domain.Abstractions.Config
{
    public static class AutoMapperFixture
    {
        public static MapperConfigurationExpression Create()
        {
            var cfg = new MapperConfigurationExpression();
                //cfg.CreateMap<FromMap, ToMap>()
                //    .ForMember(dest => dest.MyProperty, opt => opt.MapFrom(src => src.MyProperty))
                //    ;
            return cfg;
        }
    }
}
