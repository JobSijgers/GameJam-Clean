using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace SliceMode
{
    public class SliceController : MonoBehaviour
    {
        private MeshSlicer _slicer = new MeshSlicer();
        private Plane _plane;
        [SerializeField] private Vector3 _pointA, _pointB, _pointC;
        private Vector3 _cutCenter;
        [SerializeField] private LayerMask _cutLayerMask;
        [SerializeField, UnityEngine.Range(0.01f, 1f)] private float _cutResolution = 0.1f;

        private void Start()
        {
            Cut(_pointA, _pointB, _pointC);
        }

        private void Update()
        {
            _cutCenter = (_pointA + _pointB + _pointC) / 3;
        }

        private void Cut(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            Debug.Log("cutting");
            GeneratePlane(pointA, pointB, pointC);

            //Vector3 boxSize = new Vector3(Vector3.Distance(pointB, pointC), 0.02f, (((pointB + pointC) / 2) - pointA).magnitude);
            //Vector3 cutForward = (((pointB + pointC) / 2) - pointA).normalized;
            //Quaternion cutRotation = Quaternion.LookRotation(cutForward, _plane.normal);

            HashSet<Collider> colliders = GetCollidersWithinCut();
            //Debug.Log(colliders.Count);
            foreach (Collider collider in colliders)
            {
                Mesh[] meshSlices = _slicer.SliceMesh(collider.GetComponent<MeshFilter>().mesh, _plane);
                CreateNewCutObject(collider, meshSlices[0]);
                CreateNewCutObject(collider, meshSlices[1]);
                Destroy(collider.gameObject);
            }
        }

        //generate plane
        private void GeneratePlane(Vector3 pointA, Vector3 pointB, Vector3 pointC)
        {
            _plane = new Plane();
            _plane.Set3Points(pointA, pointB, pointC);
        }

        //get all slicable meshes that intersect
        private HashSet<Collider> GetCollidersWithinCut()
        {
            HashSet<Collider> validColliders = new HashSet<Collider>();
            RaycastHit[] previousHits = new RaycastHit[0];

            Vector3 targetPoint;
            for (float i = 0; i <= 1; i += _cutResolution)
            {
                targetPoint = Vector3.Lerp(_pointB, _pointC, i);

                RaycastHit[] currentHits = Physics.RaycastAll(_pointA, targetPoint, Vector3.Distance(_pointA, targetPoint), _cutLayerMask);
                Debug.Log(currentHits.Length);
                RaycastHit[] passedHits = previousHits.Except(currentHits).ToArray();
                foreach (RaycastHit hit in passedHits)
                {
                    validColliders.Add(hit.collider);
                }
                previousHits = currentHits;
            }

            return validColliders;
        }

        //generate both sides as their own objects
        private void CreateNewCutObject(Collider original, Mesh meshSlice)
        {
            //create new object for the cut off part
            GameObject cutoff = new GameObject(original.gameObject.name);
            Rigidbody rb = cutoff.AddComponent<Rigidbody>();
            MeshRenderer mr = cutoff.AddComponent<MeshRenderer>();
            MeshFilter mf = cutoff.AddComponent<MeshFilter>();
            MeshCollider colli = cutoff.AddComponent<MeshCollider>();
            colli.sharedMesh = meshSlice;
            colli.convex = true;
            mr.materials = original.GetComponent<MeshRenderer>().materials;
            mf.mesh = meshSlice;

            

            ////change the original object
            //GameObject alteredOriginal = original.gameObject;
            //alteredOriginal.GetComponent<MeshFilter>().mesh = meshSlice[1];
            //alteredOriginal.GetComponent<MeshCollider>().sharedMesh = meshSlice[1];
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_pointA, _pointB);
            Gizmos.DrawLine(_pointA, _pointC);
            Gizmos.DrawLine(_pointB, _pointC);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_cutCenter, _pointA);
            Gizmos.DrawLine(_cutCenter, _pointB);
            Gizmos.DrawLine(_cutCenter, _pointC);
        }

        //delete original meshes
    }
}
