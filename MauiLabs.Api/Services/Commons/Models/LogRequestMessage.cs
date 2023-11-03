using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Commons.Models
{
    public sealed class LogRequestMessage : IMappingTarget<LoggingInfo>
    {
        public string MethodName { get; set; } = default!;
        public string UserInfo { get; set; } = default!;
        public DateTime DateTime { get; set; } = default!;

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<LogRequestMessage, LoggingInfo>()
                .ForMember(p => p.MethodName, options => options.MapFrom(p => p.MethodName))
                .ForMember(p => p.UserInfo, options => options.MapFrom(p => p.UserInfo))
                .ForMember(p => p.DateTime, options => options.MapFrom(p => p.DateTime));
        }
    }
}
