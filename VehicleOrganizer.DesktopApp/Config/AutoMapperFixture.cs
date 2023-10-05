using AutoMapper;
using VehicleOrganizer.Domain.Abstractions.Enums;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.DesktopApp.Config
{
    public static class AutoMapperFixture
    {
        public static MapperConfigurationExpression Create()
        {
            var cfg = new MapperConfigurationExpression();
                //cfg.CreateMap<From, To>()
                //    .ForMember(dest => dest.ToProp, opt => opt.MapFrom(src => src.FromProp))
                //    ;
                cfg.CreateMap<Vehicle, VehicleView>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.ToString()))
                    .ForMember(dest => dest.OilType, opt => opt.MapFrom(src => src.VehicleType == VehicleType.Car ? src.OilType : "N/A"))
                    .ForMember(dest => dest.InsuranceConclusion, opt => opt.MapFrom(src => src.InsuranceConclusion.ToShortDateString()))
                    .ForMember(dest => dest.InsuranceTermination, opt => opt.MapFrom(src => src.InsuranceTermination.ToShortDateString()))
                    .ForMember(dest => dest.LatestMileage, opt => opt.MapFrom(src => src.LatestMileage + " km"))
                    .ForMember(dest => dest.DaysToInsuranceExpires, opt => opt.MapFrom(src => src.DaysToInsuranceExpires + " dni"))
                    ;
            return cfg;
        }
    }
}
