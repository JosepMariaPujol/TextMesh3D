using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    public static class MathUtility
    {
        //The value we use to avoid floating point precision issues
        //http://sandervanrossen.blogspot.com/2009/12/realtime-csg-part-1.html
        //Unity has a built-in Mathf.Epsilon;
        //But it's better to use our own so we can test different values
        public const float EPSILON = 0.00001f;

        //Clamp list indices
        //Will even work if index is larger/smaller than listSize, so can loop multiple times
        public static int ClampListIndex(int index, int listSize)
        {
            index = ((index % listSize) + listSize) % listSize;

            return index;
        }


        // Returns the determinant of the 2x2 matrix defined as
        // | x1 x2 |
        // | y1 y2 |
        //det(a_normalized, b_normalized) = sin(alpha) so it's similar to the dot product
        //Vector alignment dot det
        //Same:            1   0
        //Perpendicular:   0  -1
        //Opposite:       -1   0
        //Perpendicular:   0   1
        public static float Det2(float x1, float x2, float y1, float y2)
        {
            return x1 * y2 - y1 * x2;
        }


    }
}
