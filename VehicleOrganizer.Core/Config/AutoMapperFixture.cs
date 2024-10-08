﻿using AutoMapper;
using VehicleOrganizer.Domain.Abstractions;
using VehicleOrganizer.Domain.Abstractions.Extensions;
using VehicleOrganizer.Domain.Abstractions.Views;
using VehicleOrganizer.Infrastructure.Entities;

namespace VehicleOrganizer.Core.Config
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
                    .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name + (src.IsSold ? $" {Codes.VehicleSoldIndicator}" : string.Empty)))
                    .ForMember(dest => dest.YearOfProduction, opt => opt.MapFrom(src => src.YearOfProduction.ToString()))
                    .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.ToString()))
                    .ForMember(dest => dest.IsOilBased, opt => opt.MapFrom(src => src.VehicleType.IsOilBased()))
                    .ForMember(dest => dest.OilType, opt => opt.MapFrom(src => src.VehicleType.IsOilBased() ? src.OilType : Codes.None))
                    .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.PurchaseDate.ToShortDateString()))
                    .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToShortDateString()))
                    .ForMember(dest => dest.InsuranceConclusion, opt => opt.MapFrom(src => src.InsuranceConclusion.ToShortDateString()))
                    .ForMember(dest => dest.InsuranceTermination, opt => opt.MapFrom(src => src.InsuranceTermination.ToShortDateString()))
                    .ForMember(dest => dest.LastTechnicalReview, opt => opt.MapFrom(src => src.LastTechnicalReview.ToShortDateString()))
                    .ForMember(dest => dest.NextTechnicalReview, opt => opt.MapFrom(src => src.NextTechnicalReview.ToShortDateString()))
                    .ForMember(dest => dest.LatestMileage, opt => opt.MapFrom(src => src.LatestMileage + " km"))
                    .ForMember(dest => dest.DaysToInsuranceExpires, opt => opt.MapFrom(src => src.DaysToInsuranceExpires(DateTime.Now.Date) + " dni"))
                    .ForMember(dest => dest.DaysToNextTechnicalReview, opt => opt.MapFrom(src => src.DaysToInsuranceExpires(DateTime.Now.Date) + " dni"))
                    ;
                cfg.CreateMap<OperationalActivity, OperationalActivityView>()
                    .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.LastOperationDateOrMileageWhenPerformed, 
                                opt => opt.MapFrom(src => src.IsDateOperated ? src.LastOperationDate.ToShortDateString() : src.MileageWhenPerformed.ToString()))
                    .ForMember(dest => dest.SummaryPrompt, opt => opt.MapFrom(src => src.SummaryPrompt(DateTime.Now.Date, true)))
                    ;
            return cfg;
        }
    }
}
