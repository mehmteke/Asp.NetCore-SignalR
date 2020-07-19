using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CovidChart.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CovidChart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        private readonly CovidService covidService;

        public CovidController(CovidService covidService)
        {
            this.covidService = covidService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid) 
        {
            await covidService.SaveCovid(covid);
            //IQueryable<Covid> covids = covidService.GetList();
            //return Ok(covids);
            return Ok(covidService.GetCovidCharts());
        }

        [HttpGet]
        public IActionResult InitializeCovid() 
        {
            Random random = new Random();

            Enumerable.Range(1, 5).ToList().ForEach(x =>
            {
                foreach (Ecity ecity in Enum.GetValues(typeof(Ecity)))
                {
                    var newCovid = new Covid
                    {
                        City = ecity,
                        Count = random.Next(50, 100),
                        CovidDate = DateTime.Now.AddDays(x)
                    };
                    covidService.SaveCovid(newCovid).Wait();
                }
            }); 

            return Ok(covidService.GetCovidCharts());
        }

    }
}
