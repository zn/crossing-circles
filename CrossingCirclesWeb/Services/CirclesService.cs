using System;
using System.Drawing;
using Models;

namespace CrossingCirclesWeb.Services
{
    public class CirclesService
    {
        public const double TOLERANCE = 0.01;

        // https://gist.github.com/jupdike/bfe5eb23d1c395d8a0a1a4ddd94882ac
        // http://math.stackexchange.com/a/1367732
        public IntersectionsResult GetIntersections(Circle c1, Circle c2)
        {
            var (r1, r2) = (c1.Radius, c2.Radius);
            (double x1, double y1, double x2, double y2) = (c1.Center.X, c1.Center.Y, c2.Center.X, c2.Center.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

            if (Math.Abs(d) < TOLERANCE && Math.Abs(r1 - r2) < TOLERANCE)
            {
                return new IntersectionsResult
                {
                    IntersectType = Intersect.Same
                };
            }

            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2))
            {
                return new IntersectionsResult
                {
                    IntersectType = Intersect.NotIntersect
                };
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
                return new IntersectionsResult
                {
                    IntersectType = Intersect.OnePoint,
                    Intersections = new[] {i1}

                };
            }

            return new IntersectionsResult
            {
                IntersectType = Intersect.TwoPoint,
                Intersections = new[] { i1, i2 }
            };
        }
    }
}