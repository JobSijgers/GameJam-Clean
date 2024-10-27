using System;
using UnityEngine;

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

    public void TrySlice(SlicePlane plane)
    {
        if (plane == SlicePlane.Z && !!transform.forward.Equals(Vector3.forward))
        {
            plane = SlicePlane.X;
        }

        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, _castSize, transform.forward, out hitInfo, _castLength))
        {
            hitInfo.collider.GetComponentInParent<SlicableObject>()?.SliceObject(plane);
        }
    }

    public void TrySlice(string slice)
    {
        switch (slice)
        {
            case "Horizontal":
                TrySlice(SlicePlane.Y);
                break;
            case "Vertical":
                TrySlice(SlicePlane.Z);
                break;
            default:
                Debug.Log("invalid slice string");
                break;
        }
    }
}
