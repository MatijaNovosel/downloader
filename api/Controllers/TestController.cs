using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using api.Services;
using YoutubeExplode.Models.MediaStreams;
using YoutubeExplode;
using System.IO;
using System.IO.Compression;
using api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TestController : ControllerBase
  {
    private readonly ILastFmClient _httpService;
    private readonly IConfiguration _configuration;
    private readonly IHubContext<DownloaderHub> _hubContext;

    public TestController(ILastFmClient httpService, IConfiguration configuration, IHubContext<DownloaderHub> chatHub)
    {
      _httpService = httpService;
      _configuration = configuration;
      _hubContext = chatHub;
    }

    [HttpGet("")]
    public async Task<IActionResult> Get()
    {
      var result = await _httpService.GetTopArtists();
      return Ok(result);
    }

    [HttpGet("album")]
    public async Task<IActionResult> SearchAlbum(string name)
    {
      var result = await _httpService.SearchAlbum(name);
      return Ok(result);
    }

    [HttpGet("artist")]
    public async Task<IActionResult> SearchArtist(string name)
    {
      var result = await _httpService.SearchArtist(name);
      return Ok(result);
    }

    [HttpGet("artistImage")]
    public async Task<IActionResult> ArtistImage(string name)
    {
      var result = await _httpService.GetArtistImage(name);
      return Ok(result);
    }

    [HttpGet("artist/{mbid}")]
    public async Task<IActionResult> GetArtistInfo(string mbid)
    {
      var result = await _httpService.GetArtistInfo(mbid);
      return Ok(result);
    }

    [HttpGet("artist/album/{mbid}")]
    public async Task<IActionResult> GetArtistAlbums(string mbid)
    {
      var result = await _httpService.GetArtistAlbums(mbid);
      return Ok(result);
    }

    [HttpGet("album/{mbid}")]
    public async Task<IActionResult> GetAlbumInfo(string mbid)
    {
      var result = await _httpService.GetAlbumInfo(mbid);
      return Ok(result);
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery(Name = "tracks[]")] List<string> tracks, [FromQuery(Name = "artistName")] string artistName, [FromQuery(Name = "albumName")] string albumName)
    {
      var client = new YoutubeClient();
      var memoryStream = new MemoryStream();
      using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
      {
        foreach (string track in tracks)
        {
          var videos = await client.SearchVideosAsync($"{artistName} - {track} audio");

          await this._hubContext.Clients.All.SendAsync("fetchingSong", $"{track}");

          var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videos.First().Id);
          var streamInfo = streamInfoSet.Audio.First();
          var ext = streamInfo.Container.GetFileExtension();

          using (var mr = new MemoryStream())
          {
            await client.DownloadMediaStreamAsync(streamInfo, mr);

            await this._hubContext.Clients.All.SendAsync("downloadingSong", $"{track}");

            ZipArchiveEntry entry = archive.CreateEntry($"{track}.mp3");
            using (Stream entryStream = entry.Open())
            {
              mr.Position = 0;
              mr.CopyTo(entryStream);
            }

            await this._hubContext.Clients.All.SendAsync("downloadedSong", $"{track}");
          }
        }
      }
      memoryStream.Position = 0;
      byte[] bytes = memoryStream.ToArray();
      await memoryStream.DisposeAsync();
      return Ok(Convert.ToBase64String(bytes));
    }
  }
}
