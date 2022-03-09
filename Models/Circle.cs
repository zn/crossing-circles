using System;
using System.Drawing;

namespace Models
{
    public class Circle
    {
        public PointF Center { get; }
        public float Radius { get; }

        public Circle(int x, int y, float radius)
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