using System.Collections.Generic;
using System.Threading.Tasks;
using TripAPI.RequestModels;

namespace TripAPI.Services
{
    public interface ITripService
    {
        Task<IEnumerable<TripDTO>> GetTripsAsync();
        Task<bool> DeleteClientAsync(int idClient);
        Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO assignClientDTO);
    }
}
