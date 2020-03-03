using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using api.Services;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestController : ControllerBase
  {
    private readonly ILastFmClient _httpService;
    private readonly IConfiguration _configuration;

    public TestController(ILastFmClient httpService, IConfiguration configuration)
    {
      _httpService = httpService;
      _configuration = configuration;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
      var result = await _httpService.GetTopArtists();
      return Ok(result);
    }

    [HttpGet("searchAlbum")]
    public async Task<IActionResult> SearchAlbum(string name)
    {
      var result = await _httpService.SearchAlbum(name);
      return Ok(result);
    }

    [HttpGet("searchArtist")]
    public async Task<IActionResult> SearchArtist(string name)
    {
      var result = await _httpService.SearchArtist(name);
      return Ok(result);
    }
  }
}
