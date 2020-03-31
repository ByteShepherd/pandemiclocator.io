using Microsoft.AspNetCore.Mvc;

namespace website.pandemiclocator.io.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        [HttpPost]
        public bool Post([FromBody] Incident incident)
        {
            return true;
        }
    }
}
