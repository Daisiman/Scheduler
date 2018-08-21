using Microsoft.AspNetCore.SignalR;

namespace Scheduler.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string msg)
        {
            Clients.All.SendAsync("Send", name, msg);
        }
    }
}
