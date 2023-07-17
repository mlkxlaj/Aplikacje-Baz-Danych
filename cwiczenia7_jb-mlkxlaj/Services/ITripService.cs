using Microsoft.EntityFrameworkCore;
using Zadanie7.DTOs;
using Zadanie7.Models;

namespace Zadanie7.Services
{
    public interface ITripService
    {

        List<Trip> GetTripOrderBy();

        int post(int idTrip, ClientDTO client);

    }
}
