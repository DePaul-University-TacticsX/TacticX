using System.Collections.Generic;
using UnityEngine;

namespace TacticsX.SoundEngine
{
    public class SoundLibrary
    {
        private static SoundLibrary instance;

        private Dictionary<SoundEffectType,AudioClip> dict;

        public SoundLibrary()
        {
            dict = new Dictionary<SoundEffectType, AudioClip>();

            //Specify sound effects here
            /*
            dict[SoundEffectType.Sound_01] = (AudioClip)Resources.Load("Sounds/key01");
            dict[SoundEffectType.Sound_02] = (AudioClip)Resources.Load("Sounds/key02");
            dict[SoundEffectType.Sound_03] = (AudioClip)Resources.Load("Sounds/key03");
            dict[SoundEffectType.Sound_04] = (AudioClip)Resources.Load("Sounds/key04");
            dict[SoundEffectType.Sound_05] = (AudioClip)Resources.Load("Sounds/key05");
            dict[SoundEffectType.Sound_06] = (AudioClip)Resources.Load("Sounds/key06");
            dict[SoundEffectType.Sound_07] = (AudioClip)Resources.Load("Sounds/key07");
            */



            instance = this;
        }

        public static AudioClip GetSoundEffect(SoundEffectType soundEffectType)
        {
            return instance.dict[soundEffectType];
        }
    }
}