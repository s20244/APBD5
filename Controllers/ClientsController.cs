using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TripAPI.Services;

namespace TripAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public ClientsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpDelete("{idClient}")]
        public async Task<ActionResult> DeleteClient(int idClient)
        {
            var result = await _tripService.DeleteClientAsync(idClient);
            if (!result)
            {
                return BadRequest("Client has assigned trips.");
            }
            return NoContent();
        }
    }
}
