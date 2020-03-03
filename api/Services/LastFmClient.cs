using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Dynamic;

namespace api.Services
{
  class LastFmClient : ILastFmClient
  {
    private readonly IFlurlClient _flurlClient;
    private readonly IConfiguration _configuration;

    public LastFmClient(IConfiguration configuration)
    {
      _configuration = configuration;
      _flurlClient = new FlurlClient(_configuration.GetSection("LastFmConfig:BaseUrl").Value);
      _flurlClient.Headers.Add("User-Agent", _configuration.GetSection("LastFmConfig:UserAgent").Value);
    }

    public async Task<ExpandoObject> GetTopArtists()
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
      return result;
    }

    public async Task<ExpandoObject> SearchAlbum(string name)
    {
      var result = await _flurlClient
      .Request()
      .SetQueryParams(new
      {
        api_key = _configuration.GetSection("LastFmConfig:ApiKey").Value,
        method = "album.search",
        format = "json",
        album = name
      })
      .GetJsonAsync();
      return result;
    }

    public async Task<ExpandoObject> SearchArtist(string name)
    {
      var result = await _flurlClient
      .Request()
      .SetQueryParams(new
      {
        api_key = _configuration.GetSection("LastFmConfig:ApiKey").Value,
        method = "artist.search",
        format = "json",
        artist = name
      })
      .GetJsonAsync();
      return result;
    }
  }
}