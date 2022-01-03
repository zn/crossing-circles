using System;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

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

            Intersections = c1.GetIntersections(c2, out var intersectionType);
            IntersectionType = intersectionType;
        }
    }
}