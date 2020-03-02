using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestController : ControllerBase
  {
    private readonly IFlurlClient _flurlClient;
    private readonly IConfiguration _configuration;

    public TestController(IFlurlClientFactory clientFactory, IConfiguration configuration)
    {
      _flurlClient = clientFactory.Get(_configuration.GetSection("LastFmConfig:BaseUrl").Value);
      _configuration = configuration;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
      var result = await _flurlClient
      .Request()
      .SetQueryParams(new
      {
        api_key = _configuration.GetSection("LastFmConfig:ApiKey").Value,
        method = "chart.gettopartists",
        format = "json"
      })
      .GetJsonAsync();

      return Ok(result);
    }
  }
}
