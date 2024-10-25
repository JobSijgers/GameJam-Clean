using UnityEngine;

public class SlicableObject : MonoBehaviour
{
    [SerializeField] private Vector2[] _sliceVariations;

    public void SliceObect(Plane slice)
    {
        
    }

    /// <summary>
    /// takes a slice and attempts to find the closest
    /// </summary>
    protected Vector2 InterpretSlice(Plane slice)
    {
        Vector2 result = new Vector2();
        return result;
    }

    protected void ExecuteSlide()
    {

    }
}
