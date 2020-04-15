using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace api.Hubs
{
  public class DownloaderHub : Hub
  {
    public async Task Cancel()
    {
      // Do something
    }
  }
}