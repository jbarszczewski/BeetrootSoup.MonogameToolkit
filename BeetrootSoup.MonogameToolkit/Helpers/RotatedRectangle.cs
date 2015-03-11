using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeetrootSoup.MonogameToolkit.Helpers
{
    public class RotatedRectangle
    {
        public Rectangle CollisionRectangle;
        public float Rotation;
        public Vector2 Origin;

        public RotatedRectangle(Rectangle rectangle, float initialRotation)
        {
            CollisionRectangle = rectangle;
            Rotation = initialRotation;

            //Calculate the Rectangles origin. We assume the center of the Rectangle will
            //be the point that we will be rotating around and we use that for the origin
            Origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
        }

        public void ChangePosition(Vector2 newPosition)
        {
            this.CollisionRectangle.X = (int)newPosition.X;
            this.CollisionRectangle.Y = (int)newPosition.Y;
        }

        /// <summary>
        /// Used for changing the X and Y position of the RotatedRectangle
        /// </summary>
        /// <param name="xPositionAdjustment"></param>
        /// <param name="yPositionAdjustment"></param>
        public void ChangePosition(int xPositionAdjustment, int yPositionAdjustment)
        {
            CollisionRectangle.X += xPositionAdjustment;
            CollisionRectangle.Y += yPositionAdjustment;
        }

        /// <summary>
        /// This intersects method can be used to check a standard XNA framework Rectangle
        /// object and see if it collides with a Rotated Rectangle object
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public bool Intersects(Rectangle rectangle)
        {
            return Intersects(new RotatedRectangle(rectangle, 0.0f));
        }

        /// <summary>
        /// Check to see if two Rotated Rectangls have collided
        /// </summary>
        /// <param name="rotatedRectangle"></param>
        /// <returns></returns>
        public bool Intersects(RotatedRectangle rotatedRectangle)
        {
            //Calculate the Axis we will use to determine if a collision has occurred
            //Since the objects are rectangles, we only have to generate 4 Axis (2 for
            //each rectangle) since we know the other 2 on a rectangle are parallel.
            var aRectangleAxis = new List<Vector2>();
            aRectangleAxis.Add(UpperRightCorner() - UpperLeftCorner());
            aRectangleAxis.Add(UpperRightCorner() - LowerRightCorner());
            aRectangleAxis.Add(rotatedRectangle.UpperLeftCorner() - rotatedRectangle.LowerLeftCorner());
            aRectangleAxis.Add(rotatedRectangle.UpperLeftCorner() - rotatedRectangle.UpperRightCorner());

            //Cycle through all of the Axis we need to check. If a collision does not occur
            //on ALL of the Axis, then a collision is NOT occurring. We can then exit out 
            //immediately and notify the calling function that no collision was detected. If
            //a collision DOES occur on ALL of the Axis, then there is a collision occurring
            //between the rotated rectangles. We know this to be true by the Seperating Axis Theorem
            return aRectangleAxis.All(aAxis => IsAxisCollision(rotatedRectangle, aAxis));
        }

        /// <summary>
        /// Determines if a collision has occurred on an Axis of one of the
        /// planes parallel to the Rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        private bool IsAxisCollision(RotatedRectangle rectangle, Vector2 axis)
        {
            //Project the corners of the Rectangle we are checking on to the Axis and
            //get a scalar value of that project we can then use for comparison
            var rectangleAScalars = new List<int>
                                         {
                                             this.GenerateScalar(rectangle.UpperLeftCorner(), axis),
                                             this.GenerateScalar(rectangle.UpperRightCorner(), axis),
                                             this.GenerateScalar(rectangle.LowerLeftCorner(), axis),
                                             this.GenerateScalar(rectangle.LowerRightCorner(), axis)
                                         };

            //Project the corners of the current Rectangle on to the Axis and
            //get a scalar value of that project we can then use for comparison
            var rectangleBScalars = new List<int>
                                         {
                                             this.GenerateScalar(this.UpperLeftCorner(), axis),
                                             this.GenerateScalar(this.UpperRightCorner(), axis),
                                             this.GenerateScalar(this.LowerLeftCorner(), axis),
                                             this.GenerateScalar(this.LowerRightCorner(), axis)
                                         };

            //Get the Maximum and Minium Scalar values for each of the Rectangles
            int rectangleAMinimum = rectangleAScalars.Min();
            int rectangleAMaximum = rectangleAScalars.Max();
            int rectangleBMinimum = rectangleBScalars.Min();
            int rectangleBMaximum = rectangleBScalars.Max();

            //If we have overlaps between the Rectangles (i.e. Min of B is less than Max of A)
            //then we are detecting a collision between the rectangles on this Axis
            if (rectangleBMinimum <= rectangleAMaximum && rectangleBMaximum >= rectangleAMaximum)
            {
                return true;
            }
            else if (rectangleAMinimum <= rectangleBMaximum && rectangleAMaximum >= rectangleBMaximum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Generates a scalar value that can be used to compare where corners of 
        /// a rectangle have been projected onto a particular axis. 
        /// </summary>
        /// <param name="theRectangleCorner"></param>
        /// <param name="theAxis"></param>
        /// <returns></returns>
        private int GenerateScalar(Vector2 theRectangleCorner, Vector2 theAxis)
        {
            //Using the formula for Vector projection. Take the corner being passed in
            //and project it onto the given Axis
            float aNumerator = (theRectangleCorner.X * theAxis.X) + (theRectangleCorner.Y * theAxis.Y);
            float aDenominator = (theAxis.X * theAxis.X) + (theAxis.Y * theAxis.Y);
            float aDivisionResult = aNumerator / aDenominator;
            Vector2 aCornerProjected = new Vector2(aDivisionResult * theAxis.X, aDivisionResult * theAxis.Y);

            //Now that we have our projected Vector, calculate a scalar of that projection
            //that can be used to more easily do comparisons
            float aScalar = (theAxis.X * aCornerProjected.X) + (theAxis.Y * aCornerProjected.Y);
            return (int)aScalar;
        }

        /// <summary>
        /// Rotate a point from a given location and adjust using the Origin we
        /// are rotating around
        /// </summary>
        /// <param name="thePoint"></param>
        /// <param name="theOrigin"></param>
        /// <param name="theRotation"></param>
        /// <returns></returns>
        private Vector2 RotatePoint(Vector2 thePoint, Vector2 theOrigin, float theRotation)
        {
            Vector2 aTranslatedPoint = new Vector2();
            aTranslatedPoint.X = (float)(theOrigin.X + (thePoint.X - theOrigin.X) * Math.Cos(theRotation)
                - (thePoint.Y - theOrigin.Y) * Math.Sin(theRotation));
            aTranslatedPoint.Y = (float)(theOrigin.Y + (thePoint.Y - theOrigin.Y) * Math.Cos(theRotation)
                + (thePoint.X - theOrigin.X) * Math.Sin(theRotation));
            return aTranslatedPoint;
        }

        public Vector2 UpperLeftCorner()
        {
            Vector2 aUpperLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Top);
            aUpperLeft = RotatePoint(aUpperLeft, aUpperLeft + Origin, Rotation);
            return aUpperLeft;
        }

        public Vector2 UpperRightCorner()
        {
            Vector2 aUpperRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Top);
            aUpperRight = RotatePoint(aUpperRight, aUpperRight + new Vector2(-Origin.X, Origin.Y), Rotation);
            return aUpperRight;
        }

        public Vector2 LowerLeftCorner()
        {
            Vector2 aLowerLeft = new Vector2(CollisionRectangle.Left, CollisionRectangle.Bottom);
            aLowerLeft = RotatePoint(aLowerLeft, aLowerLeft + new Vector2(Origin.X, -Origin.Y), Rotation);
            return aLowerLeft;
        }

        public Vector2 LowerRightCorner()
        {
            Vector2 aLowerRight = new Vector2(CollisionRectangle.Right, CollisionRectangle.Bottom);
            aLowerRight = RotatePoint(aLowerRight, aLowerRight + new Vector2(-Origin.X, -Origin.Y), Rotation);
            return aLowerRight;
        }

        public int X
        {
            get { return CollisionRectangle.X; }
        }

        public int Y
        {
            get { return CollisionRectangle.Y; }
        }

        public int Width
        {
            get { return CollisionRectangle.Width; }
        }

        public int Height
        {
            get { return CollisionRectangle.Height; }
        }

    }
}
