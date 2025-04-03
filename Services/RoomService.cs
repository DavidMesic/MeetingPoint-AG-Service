using MeetingRoomReservation.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomReservation.API.Services
{
    public class RoomService
    {
        private readonly IMongoCollection<Room> _rooms;

        public RoomService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("projektwoche"));

            // Datenbankname kann hartcodiert oder ebenfalls über Konfiguration gesteuert werden
            var database = client.GetDatabase("projektwoche");

            // Name der Collection: "rooms"
            _rooms = database.GetCollection<Room>("rooms");
        }

        public async Task<List<Room>> GetAsync() =>
            await _rooms.Find(_ => true).ToListAsync();

        public async Task<Room> GetAsync(string id) =>
            await _rooms.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Room room) =>
            await _rooms.InsertOneAsync(room);

        public async Task UpdateAsync(string id, Room updatedRoom) =>
            await _rooms.ReplaceOneAsync(r => r.Id == id, updatedRoom);

        public async Task RemoveAsync(string id) =>
            await _rooms.DeleteOneAsync(r => r.Id == id);
    }
}
