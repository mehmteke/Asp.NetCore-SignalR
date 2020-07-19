using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace CovidChart.API.Hubs
{
    public class CovidHub: Hub
    {
        private readonly CovidService covidService;

        public CovidHub(CovidService covidService)
        {
            this.covidService = covidService;
        }

        public async Task GetCovidLlist() {
            await Clients.All.SendAsync("ReceiveCovidList",covidService.GetCovidCharts());
        }
    }
}
