using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string msg)
        {
            Clients.All.SendAsync("Send",name, msg);
        }
    }
}
