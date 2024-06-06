using System.Collections.Generic;
using System.Threading.Tasks;
using TripAPI.RequestModels;
using TripAPI.Repositories;

namespace TripAPI.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            return await _tripRepository.GetTripsAsync();
        }

        public async Task<bool> DeleteClientAsync(int idClient)
        {
            return await _tripRepository.DeleteClientAsync(idClient);
        }

        public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO assignClientDTO)
        {
            return await _tripRepository.AssignClientToTripAsync(idTrip, assignClientDTO);
        }
    }
}
