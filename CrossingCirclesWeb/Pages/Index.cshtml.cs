using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CrossingCirclesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace CrossingCirclesWeb.Pages
{
    public class Index : PageModel
    {

        private readonly HistoryService _historyService;
        private readonly CirclesService _circlesService;

        public Index(HistoryService historyService, CirclesService circlesService)
        {
            _historyService = historyService;
            _circlesService = circlesService;
        }

        [BindProperty]
        public CirclesModel CirclesModel { get; set; }
        
        public PointF[] Intersections { get; set; }
        public Intersect? IntersectionType { get; set; }
        public List<HistoryItem> LastHistoryItems { get; set; }

        
        public async Task OnGet()
        {
            LastHistoryItems = await _historyService.GetLast();
        }

        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            var c1 = new Circle(CirclesModel.Circle1X, CirclesModel.Circle1Y, CirclesModel.Radius1);
            var c2 = new Circle(CirclesModel.Circle2X, CirclesModel.Circle2Y, CirclesModel.Radius2);

            var result =_circlesService.GetIntersections(c1, c2);
            Intersections = result.Intersections;
            IntersectionType = result.IntersectType;
            LastHistoryItems = await _historyService.GetLast();

            await _historyService.SaveItem(new HistoryItem
            {
                CircleCenter1 = new[]{c1.Center.X, c1.Center.Y},
                CircleCenter2 = new[]{c2.Center.X, c2.Center.Y},
                CircleRadius1 = c1.Radius,
                CircleRadius2 = c2.Radius,
                InsertedDate = DateTime.UtcNow,
                IntersectType = IntersectionType.Value,
                Intersections = Array.Empty<float>()
                    .Concat(Intersections.SelectMany(x => new[] {x.X, x.Y}))
                    .ToArray()
            });
        }
    }
}