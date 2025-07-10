using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    public static class PointTriangulationIntersection
    {
        public static HalfEdgeFace2 TriangulationWalk(
            Vector2 p,
            HalfEdgeFace2 startTriangle,
            HalfEdgeData2 triangulationData,
            List<HalfEdgeFace2> visitedTriangles = null)
        {
            HalfEdgeFace2 currentTriangle = startTriangle ?? GetRandomTriangle(triangulationData);
            if (currentTriangle == null)
            {
                Debug.LogWarning("Couldn't find start triangle when walking in triangulation.");
                return null;
            }

            visitedTriangles?.Add(currentTriangle);

            const int maxIterations = 1000000;
            for (int safety = 0; safety < maxIterations; safety++)
            {
                HalfEdge2 e1 = currentTriangle.edge;
                HalfEdge2 e2 = e1.nextEdge;
                HalfEdge2 e3 = e2.nextEdge;

                if (IsPointToTheRightOrOnLine(e1.prevEdge.v.position, e1.v.position, p))
                {
                    if (IsPointToTheRightOrOnLine(e2.prevEdge.v.position, e2.v.position, p))
                    {
                        if (IsPointToTheRightOrOnLine(e3.prevEdge.v.position, e3.v.position, p))
                        {
                            visitedTriangles?.Add(currentTriangle);
                            return currentTriangle;
                        }
                        currentTriangle = e3.oppositeEdge?.face;
                    }
                    else
                    {
                        currentTriangle = e2.oppositeEdge?.face;
                    }
                }
                else
                {
                    currentTriangle = e1.oppositeEdge?.face;
                }

                if (currentTriangle == null)
                {
                    Debug.LogWarning("Point walk exited mesh bounds.");
                    return null;
                }

                visitedTriangles?.Add(currentTriangle);
            }

            Debug.LogError("Stuck in endless loop when walking in triangulation.");
            return null;
        }

        private static HalfEdgeFace2 GetRandomTriangle(HalfEdgeData2 data)
        {
            int count = data.faces.Count;
            if (count == 0) return null;

            int index = Random.Range(0, count);
            int i = 0;
            foreach (var face in data.faces)
            {
                if (i++ == index)
                    return face;
            }
            return null;
        }

        private static bool IsPointToTheRightOrOnLine(Vector2 a, Vector2 b, Vector2 p)
        {
            var pos = Geometry.IsPointLeftOnRightOfVector(a, b, p);
            return pos == LeftOnRight.Right || pos == LeftOnRight.On;
        }
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    public static class PointTriangulationIntersection
    {
        public static HalfEdgeFace2 TriangulationWalk(Vector2 p, HalfEdgeFace2 startTriangle, HalfEdgeData2 triangulationData, List<HalfEdgeFace2> visitedTriangles = null)
        {
            HalfEdgeFace2 intersectingTriangle = null;

            //If we have a triangle to start in which may speed up the algorithm
            HalfEdgeFace2 currentTriangle = null;

            //We can feed it a start triangle to sometimes make the algorithm faster
            if (startTriangle != null)
            {
                currentTriangle = startTriangle;
            }
            //Find a random start triangle which is faster than starting at the first triangle?
            else
            {
                int randomPos = Random.Range(0, triangulationData.faces.Count);
                int i = 0;

                //faces are stored in a hashset so we have to loop through them while counting
                //to find the start triangle
                foreach (HalfEdgeFace2 f in triangulationData.faces)
                {
                    if (i == randomPos)
                    {
                        currentTriangle = f;
                        break;
                    }

                    i += 1;
                }
            }

            if (currentTriangle == null)
            {
                Debug.Log("Couldn't find start triangle when walking in triangulation");
                return null;
            }

            if (visitedTriangles != null)
            {
                visitedTriangles.Add(currentTriangle);
            }

            //Start the triangulation walk to find the intersecting triangle
            int safety = 0;

            while (true)
            {
                safety += 1;

                if (safety > 1000000)
                {
                    Debug.Log("Stuck in endless loop when walking in triangulation");
                    break;
                }

                //Is the point intersecting with the current triangle?
                //We need to do 3 tests where each test is using the triangles edges
                //If the point is to the right of all edges, then it's inside the triangle
                //If the point is to the left we jump to that triangle instead
                HalfEdge2 e1 = currentTriangle.edge;
                HalfEdge2 e2 = e1.nextEdge;
                HalfEdge2 e3 = e2.nextEdge;

                if (IsPointToTheRightOrOnLine(e1.prevEdge.v.position, e1.v.position, p))
                {
                    if (IsPointToTheRightOrOnLine(e2.prevEdge.v.position, e2.v.position, p))
                    {
                        if (IsPointToTheRightOrOnLine(e3.prevEdge.v.position, e3.v.position, p))
                        {
                            intersectingTriangle = currentTriangle;       //We have found the triangle the point is in
                            break;
                        }
                        currentTriangle = e3.oppositeEdge.face;
                    }
                    else
                    {
                        currentTriangle = e2.oppositeEdge.face;
                    }
                }
                else
                {
                    currentTriangle = e1.oppositeEdge.face;
                }

                if (visitedTriangles != null)
                {
                    visitedTriangles.Add(currentTriangle);
                }
            }

            //Add the last triangle if we found it
            if (visitedTriangles != null && intersectingTriangle != null)
            {
                visitedTriangles.Add(intersectingTriangle);
            }

            return intersectingTriangle;
        }
        
        private static bool IsPointToTheRightOrOnLine(Vector2 a, Vector2 b, Vector2 p)
        {
            bool isToTheRight = false;
            LeftOnRight pointPos = Geometry.IsPointLeftOnRightOfVector(a, b, p);

            if (pointPos is LeftOnRight.Right or LeftOnRight.On)
            {
                isToTheRight = true;
            }

            return isToTheRight;
        }
    }
}
*/
