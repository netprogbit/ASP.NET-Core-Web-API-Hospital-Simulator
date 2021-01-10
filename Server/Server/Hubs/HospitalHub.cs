using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Server.Hubs
{
    /// <summary>
    /// SignalR hospital hub
    /// </summary>
    public class HospitalHub : Hub
    {
        public async Task SendAsync(string patientIdStr, string message)
        {
            await Clients.All.SendAsync(patientIdStr, message);
        }
    }
}
