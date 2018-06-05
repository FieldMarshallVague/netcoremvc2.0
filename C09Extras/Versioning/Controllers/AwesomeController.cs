using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/awesome")]
    public class AwesomeController : Controller
    {
        public IActionResult Get() => Ok("Version 1");
    }
}

namespace Versioning.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/awesome")]
    public class AwesomeController : Controller
    {
        public IActionResult Get() => Ok($"Version 2 - {Request.HttpContext.Connection.Id}");
    }
}