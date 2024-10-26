using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Serializable]
        public class Sound
        {
            public string _name;
            public AudioClip _clip;
            [Range(0f, 1f)] public float _volume = 0.7f;
            [Range(0.5f, 1.5f)] public float _pitch = 1f;
            public bool _loop;
            public bool _playOnAwake;
            public bool _disableCutoff;
            [Range(0f, 1f)] public float _cutoffRange;
            public AudioMixerGroup _mixerGroup;
            [FormerlySerializedAs("source")] [HideInInspector] public AudioSource _source;
        }

        public static AudioManager Instance;
        [SerializeField] private Sound[] _sounds;

        private void Awake() => Instance = this;
        private readonly Dictionary<string, Sound> _soundDictionary = new();

        private void Start()
        {
            //play sounds that are set to play on awake
            foreach (Sound sound in _sounds)
            {
                if (sound._clip == null)
                {
                    Debug.LogWarning($"Sound clip: {sound._name} is null");
                    continue;
                }

                _soundDictionary.Add(sound._name, sound);
                CreateAudioSource(sound);
            }

            _sounds = null;
        }

        //find sound and play it
        public void PlaySound(string soundName)
        {
            if (!_soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                return;
            }

            if (!sound._source.isPlaying)
            {
                sound._source.Play();
                return;
            }

            if (sound._disableCutoff && sound._source.time > sound._clip.length * sound._cutoffRange)
            {
                sound._source.Play();
            }
        }

        public void StopSound(string soundName)
        {
            if (!_soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                return;
            }

            sound._source.Stop();
        }

        private void CreateAudioSource(Sound sound)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound._clip;
            audioSource.volume = sound._volume;
            audioSource.pitch = sound._pitch;
            audioSource.loop = sound._loop;
            audioSource.outputAudioMixerGroup = sound._mixerGroup;
            sound._source = audioSource;
            if (sound._playOnAwake)
            {
                audioSource.Play();
            }
        }
    }
}