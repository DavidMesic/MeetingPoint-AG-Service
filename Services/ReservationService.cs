using MeetingRoomReservation.API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingRoomReservation.API.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("projektwoche"));
            var database = client.GetDatabase("projektwoche");

            // Name der Collection: "reservations"
            _reservations = database.GetCollection<Reservation>("reservations");
        }

        public async Task<List<Reservation>> GetAsync() =>
            await _reservations.Find(_ => true).ToListAsync();

        public async Task<Reservation> GetAsync(string id) =>
            await _reservations.Find(r => r.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Reservation reservation) =>
            await _reservations.InsertOneAsync(reservation);

        public async Task UpdateAsync(string id, Reservation updatedReservation) =>
            await _reservations.ReplaceOneAsync(r => r.Id == id, updatedReservation);

        public async Task RemoveAsync(string id) =>
            await _reservations.DeleteOneAsync(r => r.Id == id);
    }
}