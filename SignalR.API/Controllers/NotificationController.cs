using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.API.Hubs;

namespace SignalR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<MyHub> hubContext;

        public NotificationController(IHubContext<MyHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount(int teamCount)
        {
            await hubContext.Clients.All.SendAsync("Notify", $"Arkadaşalr Takım {teamCount} kişi olacak.");

            return  Ok();
        }

    }
}
