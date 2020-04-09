using MongoDB.Driver;
using ReservationApi.Models;
using ReservationApi.Repos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApi.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repo;

        //Session 1

        //In the code below, an IBookstoreDatabaseSettings instance is retrieved from DI via constructor 
        //injection. This technique provides access to the appsettings.json configuration values that were added 
        //in the Add a configuration model section.


        public ReservationService(IRepository<Reservation> repo)
        {
            _repo = repo;
        }

        public async Task<List<Reservation>> GetAsync() =>
           await _repo.GetAsync();

        public async Task<Reservation> GetAsync(string id) =>
            await _repo.GetAsync(id);

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _repo.InsertAsync(reservation);
            return reservation;
        }

        public async Task UpdateAsync(string id, Reservation reservationIn) =>
            await _repo.UpdateAsync(reservationIn);

        public async Task RemoveAsync(Reservation reservationIn) =>
            await _repo.DeleteAsync(reservationIn);

        public async Task RemoveAsync(string id) =>
            await _repo.DeleteAsync(id);
    }
}

