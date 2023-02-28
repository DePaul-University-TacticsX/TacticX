using System.Collections.Generic;
using UnityEngine;

namespace TacticsX.SoundEngine
{
    public class SoundLibrary
    {
        private static SoundLibrary instance;

        private Dictionary<SoundEffectType,AudioClip> soundDict;
        private Dictionary<MusicType, AudioClip> musicDict;

        public SoundLibrary()
        {
            soundDict = new Dictionary<SoundEffectType, AudioClip>();
            musicDict = new Dictionary<MusicType, AudioClip>();

            //Specify sound effects here
            /*
            soundDict[SoundEffectType.Sound_01] = (AudioClip)Resources.Load("Sounds/key01");
            soundDict[SoundEffectType.Sound_02] = (AudioClip)Resources.Load("Sounds/key02");
            soundDict[SoundEffectType.Sound_03] = (AudioClip)Resources.Load("Sounds/key03");
            soundDict[SoundEffectType.Sound_04] = (AudioClip)Resources.Load("Sounds/key04");
            soundDict[SoundEffectType.Sound_05] = (AudioClip)Resources.Load("Sounds/key05");
            soundDict[SoundEffectType.Sound_06] = (AudioClip)Resources.Load("Sounds/key06");
            soundDict[SoundEffectType.Sound_07] = (AudioClip)Resources.Load("Sounds/key07");
            */
            musicDict[MusicType.Music_01] = (AudioClip)Resources.Load("Sounds/music-01");
            


            instance = this;
        }

        public static AudioClip Get(SoundEffectType soundEffectType)
        {
            return GetInstance().soundDict[soundEffectType];
        }

        public static AudioClip Get(MusicType musicType)
        {
            return GetInstance().musicDict[musicType];
        }

        private static SoundLibrary GetInstance()
        {
            if (instance == null) instance = new SoundLibrary();
            return instance;
        }
    }
}