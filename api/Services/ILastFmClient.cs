using System.Threading.Tasks;
using System.Dynamic;

namespace api.Services
{
  public interface ILastFmClient
  { 
    Task<ExpandoObject> GetTopArtists();
    Task<ExpandoObject> SearchAlbum(string name);
    Task<ExpandoObject> SearchArtist(string name);
  }
}