using System;
using System.Collections;
using UnityEngine;

namespace DumbSlicer
{
    [Serializable]
    public struct SliceVariation
    {
        public GameObject variant;
        public SlicePlane slicePlane;

        public SliceVariation(GameObject variant, SlicePlane slicePlane)
        {
            this.variant = variant;
            this.slicePlane = slicePlane;
        }
    }

    public enum SlicePlane
    {
        X,
        Y,
        Z,
        unsliced
    }

    public class DumbSlicer : MonoBehaviour
    {
        [SerializeField] private LayerMask _sliceMask;
        [SerializeField] private float _castSize, _castLength;
        [Space]
        [SerializeField] private Transform _laserTarget, _laserStartHorizontal, _laserEndHorizontal, _laserStartVertical, _laserEndVertical;
        [SerializeField] private float _timeToSlice = 0.7f;
        [SerializeField] private AnimationCurve _sliceSpeed;
        private LineRenderer _lineRenderer;
        private Coroutine _sliceRoutine;

        public bool IsBusy
        {
            get { return _sliceRoutine != null; }
            private set { }
        }

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Debug.Log(IsBusy);
        }

        public void TrySlice(SlicePlane plane)
        {
            if (_sliceRoutine == null)
            {
                _sliceRoutine = StartCoroutine(LaserRoutine(plane));
            }
        }

        private IEnumerator LaserRoutine(SlicePlane plane)
        {
            Transform pointA = (plane == SlicePlane.Y? _laserStartHorizontal : _laserStartVertical);
            Transform pointB = (plane == SlicePlane.Y? _laserEndHorizontal : _laserEndVertical);
            _lineRenderer.SetPosition(0, transform.position);

            float time = 0f;
            float percent = 0f;
            while (time < _timeToSlice)
            {
                percent = _sliceSpeed.Evaluate(time / _timeToSlice);
                _laserTarget.transform.position = Vector3.Lerp(pointA.position, pointB.position, percent);

                _lineRenderer.SetPosition(1, _laserTarget.position);
                time += Time.deltaTime;
                yield return null;
            }

            _laserTarget.transform.position = transform.position;
            _lineRenderer.SetPosition(1, _laserTarget.position);
            MakeCut(plane);
            _sliceRoutine = null;
        }

        public void MakeCut(SlicePlane plane)
        {
            if (plane == SlicePlane.X && !!transform.forward.Equals(Vector3.forward))
            {
                plane = SlicePlane.Z;
            }

            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, _castSize, transform.forward, out hitInfo, _castLength))
            {
                hitInfo.collider.GetComponentInParent<SlicableObject>()?.SliceObject(plane);
            }
        }
    }
}
