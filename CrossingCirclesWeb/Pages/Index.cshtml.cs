using System;
using System.Drawing;
using CrossingCircles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CrossingCirclesWeb.Pages
{
    public class Index : PageModel
    {
        [BindProperty]
        public CirclesModel CirclesModel { get; set; }
        
        public PointF[] Intersections { get; set; }
        public Intersect? IntersectionType { get; set; }

        
        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            var c1 = new Circle(CirclesModel.Circle1X, CirclesModel.Circle1Y, CirclesModel.Radius1);
            var c2 = new Circle(CirclesModel.Circle2X, CirclesModel.Circle2Y, CirclesModel.Radius2);
            
            Intersect intersectionType;
            Intersections = getCircleIntersections(c1, c2, out intersectionType);
            IntersectionType = intersectionType;
        }
        
        private PointF[] getCircleIntersections(Circle c1, Circle c2, out Intersect intersect) 
        {
             double TOLERANCE = 0.0001;
            
            var (r1, r2) = (c1.Radius, c2.Radius);
            (double x1, double y1, double x2, double y2) = (c1.Center.X, c1.Center.Y, c2.Center.X, c2.Center.Y);
            // d = distance from center1 to center2
            double d = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));

            if (d == 0 && Math.Abs(r1 - r2) < TOLERANCE)
            {
                intersect = Intersect.Same;
                return Array.Empty<PointF>();
            }
            
            // Return an empty array if there are no intersections
            if (!(Math.Abs(r1 - r2) <= d && d <= r1 + r2))
            {
                intersect = Intersect.NotIntersect;
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

            var i1 = new PointF((float)(fx + gx), (float)(fy + gy));
            var i2 = new PointF((float)(fx - gx), (float)(fy - gy));

            if (i1 == i2)
            {
                intersect = Intersect.OnePoint;
                return new[] {i1};
            }

            intersect = Intersect.TwoPoint;
            return new[] { i1, i2 };
        }
    }
}