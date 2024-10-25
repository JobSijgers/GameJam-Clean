using UnityEngine;

public class SlicableObject : MonoBehaviour
{
    [SerializeField] private Vector2[] _sliceVariations;

    public void SliceObect(Plane slice)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    protected Plane InterpretSlice()
    {
        Plane result = new Plane();
        return result;
    }

    protected void ExecuteSlide()
    {

    }
}
