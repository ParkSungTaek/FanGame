using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _audioSource = null;            // 오디오 소스
        [SerializeField] 
        private AudioClip   _audioClip   = null;            // 오디오 클립
        [SerializeField]
        private Sounds      _soundType   = Sounds.MaxCount; // 오디오 타입


        public AudioSource audioSource { get { return GetAudioSource(); } }                       // 오디오 소스
        public Sounds      soundType   { get { return _soundType; } set { _soundType = value; } } // 오디오 타입

        private AudioSource GetAudioSource()
        {
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
            return _audioSource;    
        }

        
        /// <summary>
        /// 현재 플레이 컨트롤 중인 AudioClip
        /// </summary>
        /// <returns></returns>
        public AudioClip GetAudioClip()
        {
            return _audioClip;
        }

        /// <summary>
        /// PlayOneShot (AudioClip 관리 X)
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlayAudioOneShot(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        /// <summary>
        /// Play (AudioClip 관리 O)
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlayAudio(AudioClip audioClip)
        {
            _audioClip = audioClip;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        /// <summary>
        /// Stop AudioSource
        /// </summary>
        public void StopAudio()
        {
            audioSource.Stop();
            _audioClip = null;
            audioSource.clip = null;
        }

        /// <summary>
        /// PauseaudioSource
        /// </summary>
        public void PauseAudio()
        {
            audioSource.Pause();
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }

    }
}