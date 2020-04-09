using ReservationApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationApi.Services
{


    //Session 1
    public interface IReservationService
    {
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<List<Reservation>> GetAsync();
        Task<Reservation> GetAsync(string id);
        Task RemoveAsync(Reservation reservationIn);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Reservation reservationIn);
    }
}