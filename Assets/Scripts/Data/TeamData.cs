using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Freebie;

namespace TacticsX.Data
{
    [System.Serializable]
    public class TeamData {
        public string[] characterNames = new string[3];
        public int[] characterAlts = new int[3];

        public TeamData(CharacterSelectButton[] team) {
            for (int i = 0; i < team.Length; i++) {
                Debug.Log("team char " + i + " name: " + team[i].characterName);
                Debug.Log("team char " + i + " index: " + team[i].currentIconIndex);
                characterNames[i] = team[i].characterName;
                characterAlts[i] = team[i].currentIconIndex;
            }
        }
    }
}
