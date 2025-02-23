using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class AudioSystem 
    {
        private static AudioSystem _instance = null;      // AudioSystem 객체
        private AudioPlayer[] _audioPlayer = null; // AudioPlayer Systems
        private GameObject _audioRoot = null;

        #region 생성자
        AudioSystem() { }
        #endregion 생성자

        /// <summary>
        /// AudioSystem 객체
        /// </summary>
        public static AudioSystem Instance { get { Init(); return _instance; } }

        /// <summary>
        /// 초기화
        /// </summary>
        private static void Init()
        {
            if (_instance == null)
            {
                _instance = new AudioSystem();
                _instance._audioPlayer = new AudioPlayer[(int)Sounds.MaxCount];
                _instance._audioRoot = new GameObject { name = "AudioRoot" };
                GameObject.DontDestroyOnLoad(_instance._audioRoot);
                for (Sounds sound = 0; sound < Sounds.MaxCount; sound++)
                {
                    GameObject audioObject = new GameObject { name = $"{sound}Player" };
                    AudioPlayer audioPlayer = audioObject.AddComponent<AudioPlayer>();
                    audioPlayer.soundType = sound;
                    if (sound == Sounds.BGM)
                    {
                        audioPlayer.audioSource.loop = true;
                    }
                    audioPlayer.SetVolume(AudioManager.Instance.GetValue(sound));
                    _instance._audioPlayer[(int)sound] = audioPlayer;
                    audioObject.transform.parent = _instance._audioRoot.transform;
                }
            }
        }

        /// <summary>
        /// 오디오 플레이어 
        /// </summary>
        /// <param name="audioType"></param>
        /// <returns></returns>
        public AudioPlayer GetAudioPlayer(Sounds audioType)
        {
            return Instance._audioPlayer[(int)audioType];
        }


    }
}