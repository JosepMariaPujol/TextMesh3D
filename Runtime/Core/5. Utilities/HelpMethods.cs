using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
//Standardized methods that are the same for all
    public static class HelpMethods
    {
        //
        // Orient triangles so they have the correct orientation
        //
        public static HashSet<Triangle2> OrientTrianglesClockwise(HashSet<Triangle2> triangles)
        {
            //Convert to list or we will no be able to update the orientation
            List<Triangle2> trianglesList = new List<Triangle2>(triangles);

            for (int i = 0; i < trianglesList.Count; i++)
            {
                Triangle2 triangle = trianglesList[i];

                if (!Geometry.IsTriangleOrientedClockwise(triangle.p1, triangle.p2, triangle.p3))
                {
                    triangle.ChangeOrientation();
                    trianglesList[i] = triangle;
                }
            }

            //Back to hashset
            triangles = new HashSet<Triangle2>(trianglesList);

            return triangles;
        }
    }
}
