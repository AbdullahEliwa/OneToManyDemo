using AutoMapper;
using Dto.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.AutoMapper
{
    public static class AutoMapperManager
    {
        public static IMapper Mapper => _lazyMapper.Value;
        private static readonly Lazy<IMapper> _lazyMapper = new Lazy<IMapper>(() =>
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<CustomerProfile>();
                config.AddProfile<AddressProfile>();
            });

            var mapper = configuration.CreateMapper();
            return mapper;
        });

    }
}
