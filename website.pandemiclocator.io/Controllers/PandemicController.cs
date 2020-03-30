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
            for (int i = 1; i <= 6; i++)
            {
                bool isDeath = (i + 1) % 2 == 0;
                incidents.Add(new Incident()
                {
                    Id = i + 1,
                    Title = isDeath ? "Mortes" : "Incidentes",
                    Type = isDeath ? "death" : "incident",
                    Lat = lat + (0.04 * i),
                    Lng = lng + (0.04 * i),
                    Qtd = new Random().Next(200)
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
