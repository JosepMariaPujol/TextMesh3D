using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    public enum IntersectionCases
    {
        IsInside,
        IsOnEdge,
        NoIntersection
    }

    public static class Intersections
    {
        public static bool LineLine(Edge2 a, Edge2 b, bool includeEndPoints)
        {
            float epsilon = MathUtility.EPSILON;

            Vector2 a1 = a.p1;
            Vector2 a2 = a.p2;
            Vector2 b1 = b.p1;
            Vector2 b2 = b.p2;

            float denominator = (b2.y - b1.y) * (a2.x - a1.x) - (b2.x - b1.x) * (a2.y - a1.y);

            if (Mathf.Abs(denominator) <= epsilon)
                return false; // Lines are parallel or coincident

            float uA = ((b2.x - b1.x) * (a1.y - b1.y) - (b2.y - b1.y) * (a1.x - b1.x)) / denominator;
            float uB = ((a2.x - a1.x) * (a1.y - b1.y) - (a2.y - a1.y) * (a1.x - b1.x)) / denominator;

            if (includeEndPoints)
            {
                return (uA >= -epsilon && uA <= 1f + epsilon) && (uB >= -epsilon && uB <= 1f + epsilon);
            }
            else
            {
                return (uA > epsilon && uA < 1f - epsilon) && (uB > epsilon && uB < 1f - epsilon);
            }
        }

        public static IntersectionCases PointCircle(Vector2 a, Vector2 b, Vector2 c, Vector2 testPoint)
        {
            Vector2 center = Geometry.CalculateCircleCenter(a, b, c);
            float radiusSqr = (a - center).sqrMagnitude;
            float distSqr = (testPoint - center).sqrMagnitude;

            float tolerance = MathUtility.EPSILON * 2f;

            if (distSqr < radiusSqr - tolerance)
                return IntersectionCases.IsInside;

            if (distSqr > radiusSqr + tolerance)
                return IntersectionCases.NoIntersection;

            return IntersectionCases.IsOnEdge;
        }
    }
}
/*
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    public enum IntersectionCases
    {
        IsInside,
        IsOnEdge,
        NoIntersection
    }

    public static class Intersections
    {
        public static bool LineLine(Edge2 a, Edge2 b, bool includeEndPoints)
        {
            float epsilon = MathUtility.EPSILON;
            bool isIntersecting = false;
            float denominator = (b.p2.y - b.p1.y) * (a.p2.x - a.p1.x) - (b.p2.x - b.p1.x) * (a.p2.y - a.p1.y);

            //Make sure the denominator is != 0 (or the lines are parallel)
            if (denominator > 0f + epsilon || denominator < 0f - epsilon)
            {
                float uA = ((b.p2.x - b.p1.x) * (a.p1.y - b.p1.y) - (b.p2.y - b.p1.y) * (a.p1.x - b.p1.x)) / denominator;
                float uB = ((a.p2.x - a.p1.x) * (a.p1.y - b.p1.y) - (a.p2.y - a.p1.y) * (a.p1.x - b.p1.x)) / denominator;

                //Are the line segments intersecting if the end points are the same
                if (includeEndPoints)
                {
                    //The only difference between endpoints not included is the =, which will never happen so we have to subtract 0 by epsilon
                    float zero = 0f - epsilon;
                    float one = 1f + epsilon;

                    //Are intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                    if (uA >= zero && uA <= one && uB >= zero && uB <= one)
                    {
                        isIntersecting = true;
                    }
                }
                else
                {
                    float zero = 0f + epsilon;
                    float one = 1f - epsilon;

                    //Are intersecting if u_a and u_b are between 0 and 1
                    if (uA > zero && uA < one && uB > zero && uB < one)
                    {
                        isIntersecting = true;
                    }
                }
            }

            return isIntersecting;
        }
        
        public static IntersectionCases PointCircle(Vector2 a, Vector2 b, Vector2 c, Vector2 testPoint)
        {
            //Center of circle
            Vector2 circleCenter = Geometry.CalculateCircleCenter(a, b, c);

            //The radius sqr of the circle
            //float radiusSqr = MyVector2.SqrDistance(a, circleCenter);
            float radiusSqr = Vector2.Distance(a, circleCenter) * Vector2.Distance(a, circleCenter);

            //The distance sqr from the point to the circle center
            //float distPointCenterSqr = MyVector2.SqrDistance(testPoint, circleCenter);
            float distPointCenterSqr = Vector2.Distance(testPoint, circleCenter) * Vector2.Distance(testPoint, circleCenter);

            //Add/remove a small value becuse we will never be exactly on the edge because of floating point precision issues
            //Mutiply epsilon by two because we are using sqr root???
            if (distPointCenterSqr < radiusSqr - MathUtility.EPSILON * 2f)
            {
                return IntersectionCases.IsInside;
            }
            if (distPointCenterSqr > radiusSqr + MathUtility.EPSILON * 2f)
            {
                return IntersectionCases.NoIntersection;
            }

            return IntersectionCases.IsOnEdge;
        }
    }
}
*/
