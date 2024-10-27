using DumbSlicer;
using UISystem.Core;
using UnityEngine;

namespace UISystem.Views
{
    public class GameView : View
    {
        [SerializeField] private SliceModeButton _controlViewComponent;
        [SerializeField]private SliceUi _sliceUi;
        [SerializeField] private Camera _controlCamera, _sliceCamera;

        private void Start()
        {
            _controlViewComponent._GameView= this;
            SwitchToControlMode();
        }

        public void SwitchToSliceMode()
        {
            _sliceUi.Show();
            _controlViewComponent.Hide();
            _controlCamera.gameObject.SetActive(false);
            _sliceCamera.gameObject.SetActive(true);
        }

        public void SwitchToControlMode()
        {
            if (_sliceUi.IsBusy)
                return;
            _sliceUi.Hide();
            _controlViewComponent.Show();
            _controlCamera.gameObject.SetActive(true);
            _sliceCamera.gameObject.SetActive(false);
        }
    }
}
