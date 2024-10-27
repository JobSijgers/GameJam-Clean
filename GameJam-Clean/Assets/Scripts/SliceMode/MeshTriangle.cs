using System.Collections.Generic;
using UnityEngine;

namespace SliceMode
{
    public struct MeshTriangle
    {
        //struct to hold the data for meshtriangles
        public Vector3[] verts;
        public Vector3[] normals;
        public Vector2[] uvs;

        public MeshTriangle(Vector3 vert1, Vector3 vert2, Vector3 vert3, Vector3 normal1, Vector3 normal2, Vector3 normal3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
            verts = new Vector3[3];
            normals = new Vector3[3];
            uvs = new Vector2[2];

            verts[0] = vert1;
            verts[1] = vert2;
            verts[2] = vert3;
            normals[0] = normal1;
            normals[1] = normal2;
            normals[2] = normal3;
            uvs[0] = uv1;
            uvs[1] = uv2;
            uvs[2] = uv3;
        }

        public MeshTriangle(Vector3[] verts, Vector3[] normals, Vector2[] uvs)
        {
            this.verts = verts;
            this.normals = normals;
            this.uvs = uvs;
        }
    }
}
