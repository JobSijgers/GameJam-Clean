using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SliceMode
{
    public class GeneratedMeshSlice
    {
        public List<Vector3> _verts = new List<Vector3>();
        public List<Vector3> _normals = new List<Vector3>();
        public List<Vector2> _uvs = new List<Vector2>();
        public List<List<int>> _tris = new List<List<int>>();

        public void AddTriangle(int submesh, MeshTriangle triangle)
        {
            int currentVerticeCount = _verts.Count;

            //add submesh lists if needed
            if (_tris.Count < submesh + 1)
            {
                for (int i = _tris.Count; i < submesh + 1; i++)
                {
                    _tris.Add(new List<int>());
                }
            }

            for (int i = 0; i < 3; i++)
            {
                _verts.Add(triangle.verts[i]);
                _normals.Add(triangle.normals[i]);
                _uvs.Add(triangle.uvs[i]);
                _tris[submesh].Add(currentVerticeCount + i);
            }
        }

        public Mesh GenerateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.SetVertices(_verts.ToArray());
            mesh.SetNormals(_normals.ToArray());
            mesh.SetUVs(0, _uvs.ToArray());
            mesh.SetUVs(1, _uvs.ToArray());

            mesh.subMeshCount = _tris.Count;

            for (int i = 0; i < _tris.Count; i++)
            {
                mesh.SetTriangles(_tris[i], i);
            }
            //mesh.RecalculateBounds();

            return mesh;
        }
    }
}
