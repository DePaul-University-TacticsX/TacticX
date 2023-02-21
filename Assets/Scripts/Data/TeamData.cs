using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Freebie;

[System.Serializable]
public class TeamData {
    public string[] characterNames;
    public int[] characterAlts;

    public TeamData(CharacterSelectButton[] characters) {
        for (int i = 0; i < characters.Length; i++) {
            characterNames[i] = characters[i].characterName;
            characterAlts[i] = characters[i].currentIconIndex;
        }
    }
}
