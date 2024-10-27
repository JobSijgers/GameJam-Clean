using UISystem.Core;
using UnityEngine;

namespace DumbSlicer
{
    public class SliceUi : ViewComponent
    {
        private DumbSlicer _slicer;
        private GameObject _sliceCam;
        public bool IsBusy { get { return _slicer.IsBusy; } }

        private void Awake()
        {
            _slicer = FindAnyObjectByType<DumbSlicer>();
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void Slice(string slice)
        {
            switch (slice)
            {
                case "Horizontal":
                    _slicer.TrySlice(SlicePlane.Y);
                    break;
                case "Vertical":
                    _slicer.TrySlice(SlicePlane.X);
                    break;
                default:
                    Debug.Log("invalid slice string");
                    break;
            }
        }
    }
}
