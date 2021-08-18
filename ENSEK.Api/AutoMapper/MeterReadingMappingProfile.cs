using AutoMapper;
using ENSEK.Api.Models;
using ENSEK.Data.Access.Entities;

namespace ENSEK.Api.AutoMapper
{
    public class MeterReadingMappingProfile : Profile
    {
        public MeterReadingMappingProfile()
        {
            CreateMap<MeterReadingDto, MeterReading>()
                .ForMember(d => d.AccountId, mo => mo.MapFrom(s => s.AccountId))
                .ForMember(d => d.DateTime, mo => mo.MapFrom(s => s.MeterReadingDateTime))
                .ForMember(d => d.Value, mo => mo.MapFrom(s => s.MeterReadValue));
        }
    }
}
