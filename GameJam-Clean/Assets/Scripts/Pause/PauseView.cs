using System;
using UISystem.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pause
{
    public class PauseView : View
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _creditsButton;
        
        public override void Show()
        {
            Time.timeScale = 0;
        }

        public override void Hide()
        {
            Time.timeScale = 1;
        }

        public void Restart()
        {   
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void OpenCredits()
        {
            ViewManager._Instance.ShowView<CreditsView>();
        }
    }
}