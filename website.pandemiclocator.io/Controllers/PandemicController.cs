using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace website.pandemiclocator.io.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PandemicController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Incident> Get(double lat, double lng)
        {
            var incidents = new List<Incident>();
            for (int i = 2; i <= 10; i++)
            {
                bool isDeath = i % 2 == 0;
                bool isSuspect = i % 3 == 0;
                int qtd = new Random().Next(200);
                incidents.Add(new Incident()
                {
                    Id = i,
                    Title = $"{qtd} " + (isSuspect ? "casos suspeitos" : (isDeath ? "mortes" : "casos confirmados")),
                    Type = isSuspect ? "suspect" : (isDeath ? "death" : "confirmed"),
                    Lat = lat + (0.04 * i),
                    Lng = lng + (0.04 * i),
                    Qtd = qtd
                });
            }

            return incidents;
        }
    }

    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Qtd { get; set; }
    }
}
