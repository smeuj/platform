using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nouwan.Smeuj.Framework;
using Serilog;

namespace Nouwan.Smeuj.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmeujController : ControllerBase
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLogger<SmeujController>();

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new[] {"test1", "test2"};
        }
    }
}