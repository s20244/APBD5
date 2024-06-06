using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripAPI.Models;
using TripAPI.RequestModels;

namespace TripAPI.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TripContext _context;

        public TripRepository(TripContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            return await _context.Trips
                .OrderByDescending(t => t.DateFrom)
                .Select(t => new TripDTO
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDTO
                    {
                        Name = ct.Country.Name
                    }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDTO
                    {
                        FirstName = ct.Client.FirstName,
                        LastName = ct.Client.LastName
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<bool> DeleteClientAsync(int idClient)
        {
            var client = await _context.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.IdClient == idClient);

            if (client == null || client.ClientTrips.Any())
            {
                return false;
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientDTO assignClientDTO)
        {
            var trip = await _context.Trips.FindAsync(idTrip);
            if (trip == null) return false;

            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.Pesel == assignClientDTO.Pesel);

            if (client == null)
            {
                client = new Client
                {
                    FirstName = assignClientDTO.FirstName,
                    LastName = assignClientDTO.LastName,
                    Email = assignClientDTO.Email,
                    Telephone = assignClientDTO.Telephone,
                    Pesel = assignClientDTO.Pesel
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }

            if (_context.ClientTrips.Any(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip))
            {
                return false;
            }

            _context.ClientTrips.Add(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = assignClientDTO.PaymentDate
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
