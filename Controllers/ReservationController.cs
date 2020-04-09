using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Models;
using ReservationApi.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationApi.Controllers
{


    //Session 1

    //Uses the BookService class to perform CRUD operations.
    //Contains action methods to support GET, POST, PUT, and DELETE HTTP requests.
    //Calls CreatedAtRoute in the Create action method to return an HTTP 201 response.Status 
    //code 201 is the standard response for an HTTP POST method that creates a new resource on 
    //the server.CreatedAtRoute also adds a Location header to the response. The Location header 
    //specifies the URI of the newly created book.





    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private readonly ReservationService _reservationService;


        public ReservationController(ReservationService bookService)
        {
            _reservationService = bookService;

        }
        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> Get()
        {

            var reservations = await _reservationService.GetAsync();

            Log.Information($"In My Reservation the controller:: {reservations} {DateTime.UtcNow}!");

            return reservations;
        }


        [HttpGet("{id:length(24)}", Name = "GetReservation")]
        public async Task<ActionResult<Reservation>> Get(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Create(Reservation reservation)
        {
            await _reservationService.CreateAsync(reservation);

            return CreatedAtRoute("GetReservation", new { id = reservation.Id.ToString() }, reservation);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation reservationIn)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.UpdateAsync(id, reservationIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            await _reservationService.RemoveAsync(reservation.Id);

            return NoContent();
        }
    }
}