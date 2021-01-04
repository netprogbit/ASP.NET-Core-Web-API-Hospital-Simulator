using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Server.Hubs
{
    /// <summary>
    /// SignalR hospital hub
    /// </summary>
    public class HospitalHub : Hub
    {
        public async Task SendAsync(int patientId, string message)
        {
            await Clients.All.SendAsync(patientId.ToString(), message);
        }
    }
}
