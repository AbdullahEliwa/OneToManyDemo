using AutoMapper;
using Dto.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto
{
    public static class DependencyInjection
    {
        public static void ConfigureDtos(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperManager).Assembly);
        }
    }
}
