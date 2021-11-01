using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Log;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMemoryCache _memory;

        private IConfiguration _configuration;

        private ILogUtil _log;

        protected virtual T CreateService<T>()
        {
            return (T)HttpContext.RequestServices.GetService(typeof(T));
        }

        protected IMemoryCache GetMemoryCache
        {
            get
            {
                if (_memory == null)
                {
                    _memory = CreateService<IMemoryCache>();
                    return _memory;
                }
                return _memory;
            }
        }

        protected ILogUtil GetLog
        {
            get
            {
                if (_log == null)
                {
                    _log = CreateService<ILogUtil>();
                    return _log;
                }
                return _log;
            }
        }

        protected IConfiguration GetConfiguration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = CreateService<IConfiguration>();
                    return _configuration;
                }
                return _configuration;
            }
        }
    }
}
