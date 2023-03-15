using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCintron.Pooling;

namespace TacticsX.SoundEngine
{
    public class SoundEffect : NodeBase
    {
        private AudioSource mAudioSource;

        private void Awake()
        {
            mAudioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if(mAudioSource.isPlaying == false) SoundManager.Remove(this);
        }

        public override void Wash()
        {
            mAudioSource.volume = PlayerPrefs.GetFloat("VolumeValue");
            mAudioSource.time = 0;
            mAudioSource.clip = null;
            gameObject.SetActive(false);
        }

        public void Set(SoundEffectType soundEffectType)
        {
            mAudioSource.clip = SoundLibrary.Get(soundEffectType);
            gameObject.SetActive(true);
            mAudioSource.Play();
        }
    }
}