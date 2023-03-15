using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using TacticsX.Data;

public class LevelSelectTeamManager : MonoBehaviour
{
    [HideInInspector] private const int countLevelSelectTeamMemberCards = 3;
    [HideInInspector] public LevelSelectTeamMemberCard[] team = new LevelSelectTeamMemberCard[countLevelSelectTeamMemberCards];

    void Start()
    {
        GetTeam();
        LoadTeam();
    }

    private void GetTeam() {
        for (int i = 0; i < countLevelSelectTeamMemberCards; i++) {
            //The Character List should always be the last child of Character Selection
            team[i] = transform.GetChild(i).GetComponent<LevelSelectTeamMemberCard>();
        }
    }

    public void LoadTeam() {
        TeamData teamData = SaveManager.LoadTeamData();

        if (teamData != null) {
            for (int i = 0; i < team.Length; i++) {
                team[i].currentAltIndex = 1;
                team[i].currentAltIndex = teamData.characterAlts[i];
                team[i].characterName.text = teamData.characterNames[i];
                team[i].UpdateAlt();
            }
        }
    }
}
