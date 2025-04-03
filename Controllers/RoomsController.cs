using MeetingRoomReservation.API.Models;
using MeetingRoomReservation.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomsController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Room>>> Get() =>
            await _roomService.GetAsync();

        [HttpGet("{id:length(24)}", Name = "GetRoom")]
        public async Task<ActionResult<Room>> Get(string id)
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Create(Room room)
        {
            await _roomService.CreateAsync(room);
            return CreatedAtRoute("GetRoom", new { id = room.Id }, room);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Room updatedRoom)
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            // Damit die Id nicht überschrieben wird
            updatedRoom.Id = room.Id;

            await _roomService.UpdateAsync(id, updatedRoom);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var room = await _roomService.GetAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            await _roomService.RemoveAsync(id);
            return NoContent();
        }
    }
}