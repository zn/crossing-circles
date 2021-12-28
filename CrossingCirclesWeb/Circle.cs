using System;
using System.Drawing;

namespace CrossingCircles
{
    public class Circle
    {
        public PointF Center { get; } = new PointF();
        public float Radius { get; }

        public Circle(float x, float y, float radius)
        {
            Center = new PointF(x, y);
            if (radius < 1)
            {
                throw new ArgumentException();
            }
            Radius = radius;
        }
    }
}