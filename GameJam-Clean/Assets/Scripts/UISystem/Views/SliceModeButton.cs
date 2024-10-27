using UISystem.Core;
using UISystem.Views;
using UnityEngine;

public class SliceModeButton : ViewComponent
{
    [HideInInspector] public GameView _GameView;

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public void EnterSliceMode()
    {
        _GameView.SwitchToSliceMode();
    }
}
