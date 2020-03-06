using System.Threading.Tasks;
using System.Dynamic;

namespace api.Services
{
  public interface ILastFmClient
  { 
    Task<ExpandoObject> GetTopArtists();
    Task<ExpandoObject> SearchAlbum(string name);
    Task<ExpandoObject> SearchArtist(string name);
    Task<ExpandoObject> GetArtistInfo(string mbid);
    Task<ExpandoObject> GetArtistAlbums(string mbid);
    Task<ExpandoObject> GetAlbumInfo(string mbid);
    Task<ExpandoObject> GetArtistImage(string name);
  }
}