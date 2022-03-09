using System;
using System.Drawing;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class IntersectionsResult
    {
        public Intersect IntersectType { get; set; }

        public PointF[] Intersections { get; set; } = Array.Empty<PointF>();
    }
}