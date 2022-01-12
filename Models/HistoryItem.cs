using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class HistoryItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime InsertedDate { get; set; }

        public float[] CircleCenter1 { get; set; }

        public float[] CircleCenter2 { get; set; }

        public float CircleRadius1 { get; set; }
        public float CircleRadius2 { get; set; }

        public Intersect IntersectType { get; set; }

        public float[] Intersections { get; set; }
    }
}