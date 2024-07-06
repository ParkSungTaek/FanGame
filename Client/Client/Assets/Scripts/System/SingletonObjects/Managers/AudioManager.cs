using System;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource[] _audioSources = new AudioSource[(int)Sounds.MaxCount];
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        private float[] _volume = new float[(int)Sounds.MaxCount];

        #region 생성자 
        private AudioManager() 
        {
            for (Sounds sounds = 0; sounds < Sounds.MaxCount; sounds++)
            {
                _volume[(int)sounds] = PlayerPrefs.GetFloat($"{sounds}Volume", 1f);
            }   
        }
        #endregion 생성자 

        public float GetValue(Sounds sounds)
        {
            if (_volume == null)
            {
                Debug.LogError("AudioManager Volume is NULL");
                return 0;
            }
            if (_volume.Length < (int)sounds)
            {
                Debug.LogError($"AudioManager Volume Count is less then {sounds} : {(int)sounds}");
                return 0;
            }

            return _volume[(int)sounds];
        }

        public void SetValue(Sounds sounds, float volume)
        {

            if (_volume == null)
            {
                Debug.LogError("AudioManager Volume is NULL");
                _volume = new float[(int)Sounds.MaxCount];
            }
            if (_volume.Length < (int)sounds)
            {
                Debug.LogError($"AudioManager Volume Count is less then {sounds} : {(int)sounds}");
            }

            _volume[(int)sounds] = volume;
            PlayerPrefs.SetFloat($"{sounds}Volume", _volume[(int)sounds] >= 1f ? 1f : _volume[(int)sounds]);
            
        }

        public void PlayOneShot(AudioClip audioClip, Sounds type) => Play(audioClip, type, false, true);
        public void PlayLoop(AudioClip audioClip, Sounds type) => Play(audioClip, type, true);

        public void Play(AudioClip audioClip, Sounds type, bool loop = false, bool OneShot = false)
        {
            if (audioClip == null)
                return;

            AudioPlayer audioPlayer = AudioSystem.Instance.GetAudioPlayer(type);
            if (audioPlayer != null)
            {
                if (OneShot)
                {
                    audioPlayer.PlayAudioOneShot(audioClip);
                    return;
                }
                
                audioPlayer.PlayAudio(audioClip);
                audioPlayer.audioSource.loop = loop;
            }
        }

        AudioClip GetOrAddAudioClip(string path, SystemEnum.Sounds type = SystemEnum.Sounds.SFX)
        {
            if (path.Contains("Sounds/") == false)
                path = $"Sounds/{path}";

            AudioClip audioClip = null;

            if (type == SystemEnum.Sounds.BGM)
            {
                audioClip = ObjectManager.Instance.Load<AudioClip>(path);
            }
            else
            {
                if (_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = ObjectManager.Instance.Load<AudioClip>(path);
                    _audioClips.Add(path, audioClip);
                }
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing ! {path}");

            return audioClip;
        }

        public void SetVolume(SystemEnum.Sounds type, float volume)
        {
            _audioSources[(int)type].volume = volume;
            AudioSystem.Instance.GetAudioPlayer(type).SetVolume(volume);
        }

        public void Clear()
        {
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
            foreach (var audioClips in _audioClips)
            {
                Resources.UnloadAsset(audioClips.Value);
            }
            _audioClips.Clear();
        }
    }
}