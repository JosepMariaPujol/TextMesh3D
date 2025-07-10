using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    //To avoid floating point precision issues, it's common to normalize all data to range 0-1
    public class Normalizer2
    {
        private float dMax;
        private AABB2 boundingBox;

        public Normalizer2(List<Vector2> points)
        {
            boundingBox = new AABB2(points);
            dMax = CalculateDMax(boundingBox);
        }

        //From "A fast algorithm for constructing Delaunay triangulations in the plane" by Sloan
        //boundingBox is the rectangle that covers all original points before normalization
        public float CalculateDMax(AABB2 boundingBox)
        {
            float dMax = Mathf.Max(boundingBox.max.x - boundingBox.min.x, boundingBox.max.y - boundingBox.min.y);

            return dMax;
        }

        //
        // Normalize stuff
        //

        public Vector2 Normalize(Vector2 point)
        {
            float x = (point.x - boundingBox.min.x) / dMax;
            float y = (point.y - boundingBox.min.y) / dMax;

            Vector2 pNormalized = new Vector2(x, y);

            return pNormalized;
        }

        public List<Vector2> Normalize(List<Vector2> points)
        {
            List<Vector2> normalizedPoints = new List<Vector2>();

            foreach (Vector2 p in points)
            {
                normalizedPoints.Add(Normalize(p));
            }

            return normalizedPoints;
        }

        //
        // UnNormalize stuff
        //

        public Vector2 UnNormalize(Vector2 point)
        {
            float x = (point.x * dMax) + boundingBox.min.x;
            float y = (point.y * dMax) + boundingBox.min.y;
            Vector2 pUnNormalized = new Vector2(x, y);

            return pUnNormalized;
        }

        //HalfEdgeData2
        public HalfEdgeData2 UnNormalize(HalfEdgeData2 data)
        {
            foreach (HalfEdgeVertex2 v in data.vertices)
            {
                Vector2 vUnNormalized = UnNormalize(v.position);
                v.position = vUnNormalized;
            }

            return data;
        }
    }
}
