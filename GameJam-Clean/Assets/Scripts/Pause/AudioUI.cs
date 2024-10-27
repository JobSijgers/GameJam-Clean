using UISystem.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pause
{
    public class AudioUI : ViewComponent
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private AudioMixer _mixer;
        
        public override void Initialize()
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
            _sfxSlider.onValueChanged.AddListener(SetSfxVolume);

            _volumeSlider.value = 1;
            _musicSlider.value = 1;
            _sfxSlider.value = 1;
        }

        private void OnDestroy()
        {
            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
            _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            _sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void SetVolume(float volume)
        {
            _mixer.SetFloat("Master", ConvertRange(volume));
        }

        private void SetMusicVolume(float volume)
        {
            _mixer.SetFloat("Music", ConvertRange(volume));
        }

        private void SetSfxVolume(float volume)
        {
            _mixer.SetFloat("SFX", ConvertRange(volume));
        }

        private float ConvertRange(float value)
        {
            if (value == 0)
            {
                return -80;
            }

            return Mathf.Log10(value) * 20;
        }
    }
}