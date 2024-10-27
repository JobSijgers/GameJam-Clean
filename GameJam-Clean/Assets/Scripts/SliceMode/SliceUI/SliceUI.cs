using UISystem.Core;
using UnityEngine;

namespace DumbSlicer
{
    public class SliceUi : ViewComponent
    {
        private DumbSlicer _slicer;
        private GameObject _sliceCam;

        private void Awake()
        {
            _slicer = FindAnyObjectByType<DumbSlicer>();
            _sliceCam = GameObject.FindGameObjectWithTag("SliceCam");
            _sliceCam.SetActive(false);
        }

        public override void Hide()
        {
            if (_slicer.IsBusy)
                return;

            gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(true);
            _sliceCam.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            _sliceCam.SetActive(true);
            Camera.main.gameObject.SetActive(false);
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
