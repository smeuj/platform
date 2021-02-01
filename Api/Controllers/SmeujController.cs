using System;
using System.Collections.Generic;
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
        public IEnumerable<string> Get()
        {
          throw new NotImplementedException();
        }
    }
}