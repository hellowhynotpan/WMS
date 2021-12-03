using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.Model.Entity;
using WMS.Model.DTO;

namespace WMS.WebApi.Common
{
    public class CustomAutoMapperProfile : Profile
    {
        public CustomAutoMapperProfile()
        {
            CreateMap<SysUser, UserDTO>()
                .ForMember(c => c.Password, opt => opt.Ignore())
                .ForMember(c => c.SmsCode, opt => opt.Ignore())
                .ForMember(c => c.ValidateCount, opt => opt.Ignore())
                .ForMember(c =>c.CreateTime , opt => opt.Ignore());
            CreateMap<UserDTO, SysUser>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SysUser, LoginRsDTO>();
            CreateMap<BaseWareHouse, BaseWareHouseDTO>();
            CreateMap<BaseCargospace, BaseCargospaceDTO>()
                .ForMember(c => c.WhId, opt => opt.Ignore());
            CreateMap<BasePart, BasePartDTO>();
            CreateMap<Inbill, InbillDTO>();
            CreateMap<InbillD, InbillDDTO>();
        }
    }
}
