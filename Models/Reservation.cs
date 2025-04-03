using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MeetingRoomReservation.API.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("roomId")]
        public string RoomId { get; set; }

        [BsonElement("roomName")]
        public string RoomName { get; set; }

        [BsonElement("startTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("endTime")]
        public DateTime EndTime { get; set; }
    }
}