using AutoMapper;
using Core.Entities;
using Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.AutoMapper.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
