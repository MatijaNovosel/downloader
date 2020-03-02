using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestController : ControllerBase
  {
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public TestController(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
      _clientFactory = clientFactory;
      _configuration = configuration;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
      var client = _clientFactory.CreateClient();
      var apiKey = _configuration.GetSection("LastFmConfig:ApiKey").Value;

      var builder = new UriBuilder("http://ws.audioscrobbler.com/2.0");
      var query = HttpUtility.ParseQueryString(builder.Query);
      query["api_key"] = apiKey;
      query["method"] = "chart.gettopartists";
      query["format"] = "json";
      builder.Query = query.ToString();
      string url = builder.ToString();

      var request = new HttpRequestMessage(HttpMethod.Get, url);
      request.Headers.Add("User-Agent", "matija_novosel");

      var response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        return Ok(response);
      }
      else
      {
        return BadRequest($"{response.StatusCode} {response.ReasonPhrase}");
      }
    }
  }
}
