using Microsoft.EntityFrameworkCore;
using Zadanie7.DTOs;
using Zadanie7.Models;

namespace Zadanie7.Services
{
    public class TripService : ITripService
    {

        private readonly BazaNaApbdContext _context;

        public TripService(BazaNaApbdContext context) {

            _context = context;

        }

        public List<Trip> GetTripOrderBy()
        {
            return _context.Trips.OrderBy(k => k.DateFrom).ToList();
        }

        public int post(int idTrip, ClientDTO client)
        {
            Console.Write("adawd");
            if(!_context.Clients.Where(c => c.Pesel == client.pesel).Any())
            {
                var newClient = new Client(client.FirstName, client.lastName, client.email, client.telephone, client.pesel);
                _context.Clients.Add(newClient);
                _context.SaveChanges();
            }
            if (!_context.Trips.Where(t => t.IdTrip == idTrip).Any())
            {
                return 1;
            }
            if (_context.ClientTrips.Where(t => t.IdClient == _context.Clients.Where(c => c.Pesel == client.pesel).First().IdClient).Any())
            { 
                return 2;
            }

            var clientTmp = _context.Clients.Where(c => c.Pesel == client.pesel).First();

            _context.ClientTrips.Add(new ClientTrip(clientTmp.IdClient, idTrip, DateTime.Now, client.paymentDate));

            _context.SaveChanges();

            return 0;
        }
    }
}
