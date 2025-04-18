﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MeetingRoomReservation.API.Models
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("capacity")]
        public int Capacity { get; set; }
    }
}