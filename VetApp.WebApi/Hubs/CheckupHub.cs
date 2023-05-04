using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VDMJasminka.WebApi.Hubs
{
    public class CheckupHub : Hub
    {
        public async Task InitiateConnection(string message)
        {
            await Clients.All.SendAsync("RecieveMessage", message);
        }   
    }
}
