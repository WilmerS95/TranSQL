using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TranSQL.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesorioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccesorioController (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task <IActionResult> GetAccesorio()
        {
            string query = @"SELECT * FROM Accesorio";
            DataTable table 

        }
    }
}
