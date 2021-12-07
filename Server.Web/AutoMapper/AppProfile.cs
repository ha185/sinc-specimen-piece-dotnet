using AutoMapper;
using DataContracts;
using Server.Core.DomainModels;
using System;

namespace Server.Web.AutoMapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            this.CreateMap<AppBaseInfo, AppBaseVm>(MemberList.Destination)
                .ForMember(dest => dest.Version, opt => opt.MapFrom(src => $"{src.Version.Major}.{src.Version.Minor}.{src.Version.Build}.{src.Version.Revision}"));

            this.CreateMap<AppInfo, AppVm>(MemberList.Destination)
                .ForMember(dest => dest.Base64Data, opt => opt.MapFrom(src => Convert.ToBase64String(src.Data)));
        }
    }
}
