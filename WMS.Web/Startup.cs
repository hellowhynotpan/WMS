using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.IRepository;
using WMS.IService;
using WMS.Repository;
using WMS.Service;
using WMS.WebApi.Common;
using static WMS.WebApi.Common.AppSettingHelper;

namespace WSM.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WSM.WebApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token}(注意两者之间是一个空格)",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }

                });
            });
            //sqlsugar IOC
            services.AddSqlSugar(new IocConfig()
            {
                //数据库连接字符串
                ConnectionString = this.Configuration["PgSqlConn"],
                DbType = (IocDbType)Convert.ToInt32(this.Configuration["DBType"]),
                IsAutoCloseConnection = true
            });
            //ioc依赖注入
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            services.AddCustomJWT();
            services.AddCustom();
            services.AddCustomCorsPolicy();
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WSM.WebApi v1"));
            app.UseCors("CustomCorsPolicy");
            /* app.UseHttpsRedirection();*/

            app.UseRouting();
            //用户认证中间件  一定要在UseAuthorization前面
            app.UseAuthentication();
            // 使用授权中间件 
            app.UseAuthorization();
            //终点 交由Controller来处理
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class IocExtend
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
    }
}
