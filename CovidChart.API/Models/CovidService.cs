using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidChart.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CovidChart.API.Models
{
    public class CovidService
    {
        private readonly AppDbContext context;
        private readonly IHubContext<CovidHub> hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }

        public IQueryable<Covid> GetList() 
        {
            return context.Covids.AsQueryable();
        }

        public async Task SaveCovid(Covid covid) 
        {
            await context.Covids.AddAsync(covid);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidCharts());
        }

        public List<CovidChart> GetCovidCharts() 
        {
            List<CovidChart> covidCharts = new List<CovidChart>();
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT TARIH, [16],[22],[41],[34],[65] FROM (SELECT City,[Count], CAST(CovidDate AS DATE) AS TARIH FROM[CovidChart].[dbo].[Covids]) AS QUERY PIVOT (SUM([COUNT]) FOR City IN([16],[22],[41],[34],[65])) AS PTABLE ORDER BY TARIH ";
                command.CommandType = System.Data.CommandType.Text;
                context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        CovidChart covidChart = new CovidChart();
                        covidChart.CovidDate = reader.GetDateTime(0).ToString();

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader.GetInt32(x))) 
                            {
                                covidChart.Counts.Add(0);
                            }
                            else
                            {
                                covidChart.Counts.Add(reader.GetInt32(x));
                            }
                            
                        });

                        covidCharts.Add(covidChart);
                    }
                }

                context.Database.CloseConnection();
                return covidCharts;
            }
        }

    }
}
