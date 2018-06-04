using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Config.Api.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        private readonly AwesomeOptions Options;
        private readonly AwesomeOptions.BazOptions bazOptions;

        public ConfigController(IOptionsSnapshot<AwesomeOptions> awesomeOptions, IOptions<AwesomeOptions.BazOptions> bazOptions)
        {
            this.Options = awesomeOptions.Value;
            this.bazOptions = bazOptions.Value;
        }

        public IActionResult Get()
        {
            return Ok($"foo: {this.Options.Foo} baz.foo: {this.bazOptions.Foo}");
        }
    }
}