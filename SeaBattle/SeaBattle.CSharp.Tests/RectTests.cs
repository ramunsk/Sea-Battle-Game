using NUnit.Framework;
using SeatBattle.CSharp;

namespace SeaBattle.CSharp.Tests
{
    public class RectTests
    {
        [Test, ExpectedException]
        public void Cannot_Create_Rect_With_Negative_Width_In_Ctor()
        {
            var rect = new Rect(0, 0, -1, 1);
        }

        [Test, ExpectedException]
        public void Cannot_Create_Rect_With_Zero_Width_In_Ctor()
        {
            var rect = new Rect(0, 0, 0, 1);
        }

        [Test]
        public void Width_Accepts_Positive_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Width = 2;

            // Assert
            Assert.AreEqual(2, rect.Width);
        }

        [Test, ExpectedException]
        public void Width_DoesNot_Accept_Negative_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Width = -1;
        }

        [Test, ExpectedException]
        public void Width_DoesNot_Accept_Zero_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Width = 0;
        }



        [Test, ExpectedException]
        public void Cannot_Create_Rect_With_Negative_Height_In_Ctor()
        {
            var rect = new Rect(0, 0, 1, -1);
        }

        [Test, ExpectedException]
        public void Cannot_Create_Rect_With_Zero_Height_In_Ctor()
        {
            var rect = new Rect(0, 0, 1, 0);
        }
        
        [Test]
        public void Height_Accepts_Positive_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Height = 2;

            // Assert
            Assert.AreEqual(2, rect.Height);
        }

        [Test, ExpectedException]
        public void Height_DoesNot_Accept_Negative_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Height = -1;
        }

        [Test, ExpectedException]
        public void Height_DoesNot_Accept_Zero_Value()
        {
            // Arrange
            var rect = new Rect(0, 0, 1, 1);

            // Act
            rect.Height = 0;
        }


        [Test]
        public void Cointains_ShouldReturn_False_For_Outside_Rect()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var outer2 = new Rect(11, 11, 5, 5);

            // Act
            var result = outer.Contains(outer2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Cointains_ShouldReturn_True_For_Inside_Rect()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(1, 1, 5, 5);

            // Act
            var result = outer.Contains(inner);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Cointains_ShouldReturn_True_For_Inside_Rect_AtTopLeftCorner()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(0, 0, 5, 5);

            // Act
            var result = outer.Contains(inner);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Cointains_ShouldReturn_True_For_Inside_Rect_AtTopRightCorner()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(5, 5, 5, 5);

            // Act
            var result = outer.Contains(inner);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Cointains_ShouldReturn_True_For_Inside_Rect_AtBottomLeftCorner()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(0, 9, 5, 1);

            // Act
            var result = outer.Contains(inner);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Cointains_ShouldReturn_True_For_Inside_Rect_AtBottomRightCorner()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(8, 8, 2, 2);

            // Act
            var result = outer.Contains(inner);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Cointains_ShouldReturn_False_For_Intersecting_Rect()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var outer2 = new Rect(-10, 5, 100, 3);

            // Act
            var result = outer.Contains(outer2);

            // Assert
            Assert.IsFalse(result);
        }



        [Test]
        public void Intersects_ShouldReturn_False_For_NonIntersectingRects()
        {
            // Arrange
            var r1 = new Rect(0, 0, 10, 10);
            var r2 = new Rect(11, 11, 10, 10);

            // Act
            var result1 = r1.IntersectsWith(r2);
            var result2 = r2.IntersectsWith(r1);

            // Assert
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [Test]
        public void Intersects_ShouldReturn_True_For_CrossingRects()
        {
            // Arrange
            var r1 = new Rect(0, 3, 10, 2);
            var r2 = new Rect(4, 0, 2, 10);

            // Act
            var result1 = r1.IntersectsWith(r2);
            var result2 = r2.IntersectsWith(r1);

            // TODO: continue here
            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Intersects_ShouldReturn_True_For_IntersectingRects_At_TopLeft_BottomRight()
        {
            // Arrange
            var r1 = new Rect(1, 1, 5, 5);
            var r2 = new Rect(3, 3, 6, 6);

            // Act
            var result1 = r1.IntersectsWith(r2);
            var result2 = r2.IntersectsWith(r1);

            // TODO: continue here
            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Intersects_ShouldReturn_True_For_IntersectingRects_At_TopRight_BottomLeft()
        {
            // Arrange
            var r1 = new Rect(4, 1, 5, 5);
            var r2 = new Rect(0, 4, 6, 6);

            // Act
            var result1 = r1.IntersectsWith(r2);
            var result2 = r2.IntersectsWith(r1);

            // TODO: continue here
            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Intersects_ShouldReturn_True_For_Contained_Rect()
        {
            // Arrange
            var outer = new Rect(0, 0, 10, 10);
            var inner = new Rect(5, 5, 2, 2);

            // Act
            var result1 = outer.IntersectsWith(inner);
            var result2 = inner.IntersectsWith(outer);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void Intersects_ShouldReturn_False_For_Touching_Rects()
        {
            // Arrange
            var rect = new Rect(0, 0, 10, 10);
            var n = new Rect(0, -10, 10, 10);
            var ne = new Rect(10, -10, 10, 10);
            var e = new Rect(10,0,10,10);
            var se = new Rect(10,10,10,10);
            var s = new Rect(0,10,10,10);
            var sw = new Rect(-10,10,10,10);
            var w = new Rect(-10,0,10,10);
            var nw = new Rect(-10,-10,10,10);

            // Act
            var result1 = rect.IntersectsWith(n);
            var result2 = rect.IntersectsWith(ne);
            var result3 = rect.IntersectsWith(e);
            var result4 = rect.IntersectsWith(se);
            var result5 = rect.IntersectsWith(s);
            var result6 = rect.IntersectsWith(sw);
            var result7 = rect.IntersectsWith(w);
            var result8 = rect.IntersectsWith(nw);


            // Assert
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
            Assert.IsFalse(result3);
            Assert.IsFalse(result4);
            Assert.IsFalse(result5);
            Assert.IsFalse(result6);
            Assert.IsFalse(result7);
            Assert.IsFalse(result8);

        }
    }
}