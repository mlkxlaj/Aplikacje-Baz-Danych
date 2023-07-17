using Zadanie7.Models;

namespace Zadanie7.Services
{
    public class ClientsService : IClientsService
    {

        private readonly BazaNaApbdContext _context;

        public ClientsService(BazaNaApbdContext context)
        {
            _context = context;
        }

        public int deleteClients(int id)
        {
            var tmp = _context.Clients.Where(a => a.IdClient == id).FirstOrDefault();

            if (tmp == null)
            {
                return 1;
            }

            var tmp2 = _context.ClientTrips.Where(a => a.IdClient == id).FirstOrDefault();

            if (tmp2 != null)
            {
                return 2;
            }
           
               _context.Clients.Remove(tmp);
               _context.SaveChanges();
               return 3;
            
        }
    }
}
