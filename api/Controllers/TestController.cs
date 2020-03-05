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
      using (FileStream zip = new FileStream($"{albumName}.zip", FileMode.Create))
      {
        using (ZipArchive archive = new ZipArchive(zip, ZipArchiveMode.Update))
        {
          foreach (string track in tracks)
          {
            var videos = await client.SearchVideosAsync($"{artistName} - {track} audio");
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videos.First().Id);
            var streamInfo = streamInfoSet.Audio.First();
            var ext = streamInfo.Container.GetFileExtension();
            using (var mr = new MemoryStream())
            {
              await client.DownloadMediaStreamAsync(streamInfo, mr);
              ZipArchiveEntry entry = archive.CreateEntry($"{track}.mp3");
              using (Stream entryStream = entry.Open())
              {
                mr.Position = 0;
                mr.CopyTo(entryStream);
              }
            }
          }
        }
      }
      return Ok();
    }
  }
}
