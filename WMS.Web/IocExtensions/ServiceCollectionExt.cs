using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Log;
using WMS.IRepository;
using WMS.IService;
using WMS.Repository;
using WMS.Service;

namespace WMS.WebApi.IocExtensions
{
    /// <summary>
    /// IOC拓展
    /// </summary>
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddCustom(this IServiceCollection services)
        {
            //作用域 （Scoped）在每次请求中只创建一次。
            services.AddScoped<ISysUserRepository, SysUserRepository>();
            services.AddScoped<ISysUserService, SysUserService>();

            services.AddScoped<ISysUserLogOnRepository, SysUserLogOnRepository>();
            services.AddScoped<ISysUserLogOnService, SysUserLogOnService>();

            services.AddScoped<IStockMRepository, StockMRepository>();
            services.AddScoped<IStockMService, StockMService>();

            services.AddScoped<IStockMRepository, StockMRepository>();
            services.AddScoped<IStockMService, StockMService>();

            services.AddScoped<IStockDRepository, StockDRepository>();
            services.AddScoped<IStockDService, StockDService>();

            services.AddScoped<IInbillRepository, InbillRepository>();
            services.AddScoped<IInbillService, InbillService>();

            services.AddScoped<IBaseWareHouseRepository, BaseWareHouseRepository>();
            services.AddScoped<IBaseWareHouseService, BaseWareHouseService>();

            services.AddScoped<IBasePartRepository, BasePartRepository>();
            services.AddScoped<IBasePartService, BasePartService>();

            services.AddScoped<IBaseCargospaceRepository, BaseCargospaceRepository>();
            services.AddScoped<IBaseCargospaceService, BaseCargospaceService>();

            services.AddScoped<IBaiDuFaceMService, BaiDuFaceMService>();
            return services;
        }
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDCD-DSADA-FSA-GFDAG-GDMDEAI-VF")),
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:5000",
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:5000",
                    };
                });
            return services;
        }
        public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            return services;
        }

        /// <summary>
        /// Nlog日志
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNlog(this IServiceCollection services)
        {
            return services.AddSingleton<ILogUtil, NlogUtil>();
        }
    }
}
