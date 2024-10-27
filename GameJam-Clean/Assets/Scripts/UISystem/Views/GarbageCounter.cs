using TMPro;
using UISystem.Core;
using UnityEngine;

namespace UISystem.Views
{
    public class GarbageCounter : ViewComponent 
    {
        [SerializeField] private TMP_Text _garbageCounterText;
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void UpdateGarbageCount(int garbageCount)
        {
            _garbageCounterText.text = "Garbage left: " + garbageCount;
        }
    }
}