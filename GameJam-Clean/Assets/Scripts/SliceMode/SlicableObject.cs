using UnityEngine;

public class SlicableObject : MonoBehaviour
{
    private SlicePlane currentSlicePlane = SlicePlane.unsliced;
    [SerializeField] private SliceVariation[] sliceVariations;

    public void SliceObject(SlicePlane plane)
    {
        if (currentSlicePlane == SlicePlane.unsliced)
        {
            foreach (SliceVariation variation in sliceVariations)
            {
                variation.variant.SetActive(variation.slicePlane == plane);
            }
        }
    }
}
