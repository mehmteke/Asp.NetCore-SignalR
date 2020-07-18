using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.API.Hubs
{
    public class MyHub:Hub
    {
        public static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;


        public async Task SendName(string name)
        {
            if (Names.Count > 5) 
            {
                await Clients.Caller.SendAsync("Error", $"Takım en fazla 5 Kişi Olabilir");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }
        }

        public async Task GetName()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        public async override Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount",ClientCount);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount",ClientCount);
            await base.OnDisconnectedAsync(exception);
        }


    }
}
