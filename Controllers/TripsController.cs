using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripAPI.RequestModels;
using TripAPI.Services;

namespace TripAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
        {
            var trips = await _tripService.GetTripsAsync();
            return Ok(trips);
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<ActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientDTO assignClientDTO)
        {
            var result = await _tripService.AssignClientToTripAsync(idTrip, assignClientDTO);
            if (!result)
            {
                return BadRequest("Failed to assign client to trip.");
            }
            return Ok();
        }
    }
}
