using MeetingRoomReservation.API.Models;
using MeetingRoomReservation.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> Get() =>
            await _reservationService.GetAsync();

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
            // Optional: Prüfung, ob sich die Zeiten überschneiden
            // Optional: Prüfung, ob "roomId" und "roomName" zusammenpassen

            await _reservationService.CreateAsync(reservation);
            return CreatedAtRoute("GetReservation", new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reservation updatedReservation)
        {
            var reservation = await _reservationService.GetAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            updatedReservation.Id = reservation.Id;

            await _reservationService.UpdateAsync(id, updatedReservation);
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

            await _reservationService.RemoveAsync(id);
            return NoContent();
        }
    }
}