using CrossingCirclesWeb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class CrossingCirclesTests
    {
        private readonly CirclesService _circlesService;
        public CrossingCirclesTests()
        {
            _circlesService = new CirclesService();
        }

        [TestMethod]
        public void NoIntersections()
        {
            var c1 = new Circle(3, 4, 5);
            var c2 = new Circle(-5, -6, 7);

            var result = _circlesService.GetIntersections(c1, c2);
            
            Assert.AreEqual(Intersect.NotIntersect, result.IntersectType);
            Assert.AreEqual(0, result.Intersections.Length);
        }

        [TestMethod]
        public void OneIntersection()
        {
            var c1 = new Circle(0, 0, 9);
            var c2 = new Circle(12, 0, 3);

            var result = _circlesService.GetIntersections(c1, c2);
            
            Assert.AreEqual(Intersect.OnePoint, result.IntersectType);
            Assert.AreEqual(1, result.Intersections.Length);
            Assert.AreEqual(9, result.Intersections[0].X);
            Assert.AreEqual(0, result.Intersections[0].Y);
        }

        [TestMethod]
        public void TwoIntersections()
        {
            var c1 = new Circle(2, 3, 5);
            var c2 = new Circle(0, 0, 6);

            var result = _circlesService.GetIntersections(c1, c2);
            
            Assert.AreEqual(Intersect.TwoPoint, result.IntersectType);
            Assert.AreEqual(2, result.Intersections.Length);
            
            Assert.IsTrue(result.Intersections[0].X - (-2.31) < CirclesService.TOLERANCE);
            Assert.IsTrue(result.Intersections[0].Y - 5.54 < CirclesService.TOLERANCE);
            
            Assert.IsTrue(result.Intersections[1].X - 6 < CirclesService.TOLERANCE);
            Assert.IsTrue(result.Intersections[1].Y - 0 < CirclesService.TOLERANCE);
        }

        [TestMethod]
        public void SameCircles()
        {
            var c1 = new Circle(0, 0, 10);
            var c2 = new Circle(0, 0, 10);

            var result = _circlesService.GetIntersections(c1, c2);
            
            Assert.AreEqual(Intersect.Same, result.IntersectType);
            Assert.AreEqual(0, result.Intersections.Length);
        }
    }
}