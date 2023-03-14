using UnityEngine;

namespace TacticsX.SoundEngine
{
    public class MusicManager
    {
        private static MusicManager instance;
        private AudioSource source;

        public MusicManager()
        {
            source = new GameObject("Music").AddComponent<AudioSource>();
            source.loop = true;
            Object.DontDestroyOnLoad(source.gameObject);
        }

        public static void Play(MusicType musicType)
        {
            Stop();

            AudioSource source = GetInstance().source;
            source.volume = PlayerPrefs.GetFloat("VolumeValue");
            source.clip = SoundLibrary.Get(musicType);
            GetInstance().source.Play();
        }

        public static void Stop()
        {
            GetInstance().source.Stop();
        }

        public static void SetVolume(float f)
        {
            GetInstance().source.volume = f;
        }

        private static MusicManager GetInstance()
        {
            if (instance == null) instance = new MusicManager();
            return instance;
        }
    }
}