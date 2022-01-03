using System;
using System.Drawing;

namespace Models
{
    public class Circle
    {
        public const double TOLERANCE = 0.01;

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

        public PointF[] GetIntersections(Circle anotherCircle, out Intersect intersectType)
        {
            var (r1, r2) = (this.Radius, anotherCircle.Radius);
            (double x1, double y1, double x2, double y2) = (this.Center.X, this.Center.Y, anotherCircle.Center.X,
                anotherCircle.Center.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

            if (d == 0 && Math.Abs(r1 - r2) < TOLERANCE)
            {
                intersectType = Intersect.Same;
                return Array.Empty<PointF>();
            }

            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2))
            {
                intersectType = Intersect.NotIntersect;
                return Array.Empty<PointF>();
            }

            // Intersections i1 and possibly i2 exist
            var dsq = d * d;
            var (r1sq, r2sq) = (r1 * r1, r2 * r2);
            var r1sq_r2sq = r1sq - r2sq;
            var a = r1sq_r2sq / (2 * dsq);
            var c = Math.Sqrt(2 * (r1sq + r2sq) / dsq - (r1sq_r2sq * r1sq_r2sq) / (dsq * dsq) - 1);

            var fx = (x1 + x2) / 2 + a * (x2 - x1);
            var gx = c * (y2 - y1) / 2;

            var fy = (y1 + y2) / 2 + a * (y2 - y1);
            var gy = c * (x1 - x2) / 2;

            var i1 = new PointF((float) (fx + gx), (float) (fy + gy));
            var i2 = new PointF((float) (fx - gx), (float) (fy - gy));

            if (i1 == i2)
            {
                intersectType = Intersect.OnePoint;
                return new[] {i1};
            }

            intersectType = Intersect.TwoPoint;
            return new[] {i1, i2};
        }
    }
}