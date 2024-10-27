using System.Collections.Generic;
using UnityEngine;

namespace SliceMode
{
    public class MeshSlicer
    {
        private Plane _plane;
        private Mesh _originalMesh;
        private GeneratedMeshSlice _sliceA, _sliceB;

        public Mesh[] SliceMesh(Mesh mesh, Plane plane)
        {
            _plane = plane;
            _originalMesh = mesh;

            _sliceA = new GeneratedMeshSlice();
            _sliceB = new GeneratedMeshSlice();
            SortTriangles(mesh, plane);

            Mesh[] slices = { _sliceA.GenerateMesh(), _sliceB.GenerateMesh() };
            return slices;
        }

        private void SortTriangles(Mesh original, Plane plane)
        {
            //loop over every submesh
            for (int submeshIndex = 0; submeshIndex < original.subMeshCount; submeshIndex++)
            {
                //loop over the tris in a submesh
                int[] submeshTris = original.GetTriangles(submeshIndex);
                for (int triIndex = 0; triIndex < submeshTris.Length; triIndex+=3)
                {
                    //checks if triangle is on the left or right side of the plane and sorts it
                    int triIndexA = submeshTris[triIndex];
                    int triIndexB = submeshTris[triIndex+1];
                    int triIndexC = submeshTris[triIndex+2];
                    MeshTriangle currentTriangle = GetTriangleFromMesh(original, triIndexA, triIndexB, triIndexC);

                    bool triVertIsLeftA = plane.GetSide(original.vertices[triIndexA]);
                    bool triVertIsLeftB = plane.GetSide(original.vertices[triIndexB]);
                    bool triVertIsLeftC = plane.GetSide(original.vertices[triIndexC]);

                    //sort into meshslice if tri is fully on one side
                    //generate new tris and sort those if its intersected
                    //if (triVertIsLeftA == triVertIsLeftB == triVertIsLeftC)
                    //{
                    //    if (triVertIsLeftA)
                    //    {
                            _sliceA.AddTriangle(submeshIndex, currentTriangle);
                            Debug.Log("added to a");
                    //    }
                    //    else
                    //    {
                    //        _sliceB.AddTriangle(submeshIndex, currentTriangle);
                    //       Debug.Log("added to b");
                    //    }
                    //}
                    //else
                    //{
                    //    CutTriangle(currentTriangle, triVertIsLeftA, triVertIsLeftB, triVertIsLeftC, submeshIndex);
                    //    Debug.Log("cutting triangle");
                    //    Debug.Log($"removed triangle {triIndexA} {triIndexB} {triIndexC}");
                    //}
                }
            }
        }

        /// <summary>
        /// subdivides the triangles along the plane and returns them
        /// </summary>
        /// <returns>first array is above the plane, second is below the plane</returns>
        private MeshTriangle[][] CutTriangle(MeshTriangle tri, bool aIsLeft, bool bIsLeft, bool cIsLeft, int submeshIndex)
        {
            MeshTriangle triA = new MeshTriangle();
            MeshTriangle triB = new MeshTriangle();
            MeshTriangle triC = new MeshTriangle();

            return null;
        }

        private MeshTriangle GetTriangleFromMesh(Mesh original, int index1, int index2, int index3)
        {
            Vector3[] verts =
            {
                original.vertices[index1],
                original.vertices[index2],
                original.vertices[index3]
            };

            Vector3[] normals =
            {
                original.normals[index1],
                original.normals[index2],
                original.normals[index3]
            };

            Vector2[] uvs =
            {
                original.uv[index1],
                original.uv[index2],
                original.uv[index3]
            };

            return new MeshTriangle(verts, normals, uvs);
        }
    }
}
