using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidChart.API.Models
{
    public enum Ecity
    {
        Van = 1,
        Kocaeli = 2,
        Istanbul = 3,
        Bursa = 4,
        Ankara = 5
    }
    public class Covid
    {
        public int Id { get; set; }
        public Ecity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
