using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    [TestClass]
    public class CrossingCirclesTests
    {
        [TestMethod]
        public void NoIntersections()
        {
            var c1 = new Circle(3, 4, 5);
            var c2 = new Circle(-5, -6, 7);

            var intersections = c1.GetIntersections(c2, out var intersectType);
            
            Assert.AreEqual(Intersect.NotIntersect, intersectType);
            Assert.AreEqual(0, intersections.Length);
        }

        [TestMethod]
        public void OneIntersection()
        {
            var c1 = new Circle(0, 0, 9);
            var c2 = new Circle(12, 0, 3);

            var intersections = c1.GetIntersections(c2, out var intersectType);
            
            Assert.AreEqual(Intersect.OnePoint, intersectType);
            Assert.AreEqual(1, intersections.Length);
            Assert.AreEqual(9, intersections[0].X);
            Assert.AreEqual(0, intersections[0].Y);
        }

        [TestMethod]
        public void TwoIntersections()
        {
            var c1 = new Circle(2, 3, 5);
            var c2 = new Circle(0, 0, 6);

            var intersections = c1.GetIntersections(c2, out var intersectType);
            
            Assert.AreEqual(Intersect.TwoPoint, intersectType);
            Assert.AreEqual(2, intersections.Length);
            
            Assert.IsTrue(intersections[0].X - (-2.31) < Circle.TOLERANCE);
            Assert.IsTrue(intersections[0].Y - 5.54 < Circle.TOLERANCE);
            
            Assert.IsTrue(intersections[1].X - 6 < Circle.TOLERANCE);
            Assert.IsTrue(intersections[1].Y - 0 < Circle.TOLERANCE);
        }

        [TestMethod]
        public void SameCircles()
        {
            var c1 = new Circle(0, 0, 10);
            var c2 = new Circle(0, 0, 10);

            var intersections = c1.GetIntersections(c2, out var intersectType);
            
            Assert.AreEqual(Intersect.Same, intersectType);
            Assert.AreEqual(0, intersections.Length);
        }
    }
}