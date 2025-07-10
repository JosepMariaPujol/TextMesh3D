using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    /// <summary>
    /// Represents a 2D half-edge data structure (HEDS) used for mesh topology.
    /// </summary>
    public class HalfEdgeData2
    {
        public HashSet<HalfEdgeVertex2> vertices;
        public HashSet<HalfEdgeFace2> faces;
        public HashSet<HalfEdge2> edges;

        public HalfEdgeData2()
        {
            vertices = new HashSet<HalfEdgeVertex2>();
            faces = new HashSet<HalfEdgeFace2>();
            edges = new HashSet<HalfEdge2>();
        }
    }

    /// <summary>
    /// A 2D vertex in the half-edge structure.
    /// </summary>
    public class HalfEdgeVertex2
    {
        /// <summary>
        /// The 2D position of the vertex.
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// A half-edge that starts from this vertex (goes to another).
        /// </summary>
        public HalfEdge2 edge;

        public HalfEdgeVertex2(Vector2 position)
        {
            this.position = position;
        }

        public override int GetHashCode() => position.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is HalfEdgeVertex2 other && position == other.position;
        }
    }

    /// <summary>
    /// A face (usually triangle) in the half-edge mesh.
    /// </summary>
    public class HalfEdgeFace2
    {
        /// <summary>
        /// One of the bounding half-edges of this face.
        /// </summary>
        public HalfEdge2 edge;

        public HalfEdgeFace2(HalfEdge2 edge)
        {
            this.edge = edge;
        }

        public override int GetHashCode() => edge?.GetHashCode() ?? 0;

        public override bool Equals(object obj)
        {
            return obj is HalfEdgeFace2 other && edge == other.edge;
        }
    }

    /// <summary>
    /// A half-edge in the mesh, pointing from a vertex to its destination.
    /// </summary>
    public class HalfEdge2
    {
        /// <summary>
        /// The destination vertex of the half-edge.
        /// </summary>
        public HalfEdgeVertex2 v;

        /// <summary>
        /// The face this half-edge borders.
        /// </summary>
        public HalfEdgeFace2 face;

        /// <summary>
        /// The next half-edge in the face (clockwise).
        /// </summary>
        public HalfEdge2 nextEdge;

        /// <summary>
        /// The opposite half-edge belonging to the adjacent face.
        /// </summary>
        public HalfEdge2 oppositeEdge;

        /// <summary>
        /// (Optional) The previous half-edge in the face.
        /// </summary>
        public HalfEdge2 prevEdge;

        public HalfEdge2(HalfEdgeVertex2 v)
        {
            this.v = v;
        }

        public override int GetHashCode() => v?.position.GetHashCode() ?? 0;

        public override bool Equals(object obj)
        {
            return obj is HalfEdge2 other && v == other.v;
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Softgraph.TextMesh3D.Runtime
{
    //A collection of classes that implements the Half-Edge Data Structure
    //From https://www.openmesh.org/media/Documentations/OpenMesh-6.3-Documentation/a00010.html

    //2D space
    public class HalfEdgeData2
    {
        public HashSet<HalfEdgeVertex2> vertices;
        public HashSet<HalfEdgeFace2> faces;
        public HashSet<HalfEdge2> edges;

        public HalfEdgeData2()
        {
            vertices = new HashSet<HalfEdgeVertex2>();
            faces = new HashSet<HalfEdgeFace2>();
            edges = new HashSet<HalfEdge2>();
        }
    }
    
    //A position
    public class HalfEdgeVertex2
    {
        //The position of the vertex
        public Vector2 position;

        //Each vertex references an half-edge that starts at this point
        //Might seem strange because each halfEdge references a vertex the edge is going to?
        public HalfEdge2 edge;

        public HalfEdgeVertex2(Vector2 position)
        {
            this.position = position;
        }
    }

    //This face could be a triangle or whatever we need
    public class HalfEdgeFace2
    {
        //Each face references one of the halfedges bounding it
        //If you need the vertices, you can use this edge
        public HalfEdge2 edge;
        
        public HalfEdgeFace2(HalfEdge2 edge)
        {
            this.edge = edge;
        }
    }

    //An edge going in a direction
    public class HalfEdge2
    {
        //The vertex it points to
        public HalfEdgeVertex2 v;

        //The face it belongs to
        public HalfEdgeFace2 face;

        //The next half-edge inside the face (ordered clockwise)
        //The document says counter-clockwise but clockwise is easier because that's how Unity is displaying triangles
        public HalfEdge2 nextEdge;

        //The opposite half-edge belonging to the neighbor
        public HalfEdge2 oppositeEdge;

        //(optionally) the previous halfedge in the face
        //If we assume the face is closed, then we could identify this edge by walking forward
        //until we reach it
        public HalfEdge2 prevEdge;
        
        public HalfEdge2(HalfEdgeVertex2 v)
        {
            this.v = v;
        }
    }
}
*/
