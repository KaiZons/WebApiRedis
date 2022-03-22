using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountController : ControllerBase
    {
        private const string s_key = "count";
        private readonly ILogger<CountController> _logger;
        private readonly IDistributedCache _cache;
        public CountController(ILogger<CountController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public string Get()
        {
            string value = _cache.GetString(s_key);
            if (string.IsNullOrWhiteSpace(value))
            {
                _cache.SetString(s_key, "1");
            }
            else
            {
                int intValue = Convert.ToInt32(value) + 1;
                _cache.SetString(s_key, intValue.ToString());
            }
            return _cache.GetString(s_key);
        }
    }
}
