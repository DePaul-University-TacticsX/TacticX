using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using TacticsX.Data;

public class LevelSelectTeamMemberCard : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI characterName;
    public Sprite characterAlt0;
    public Sprite characterAlt1;
    public Sprite characterAlt2;
    [HideInInspector] public Sprite[] characterAlts = new Sprite[3];
    [HideInInspector] public int currentAltIndex = 0;

    public void Start() {
        characterAlts[0] = characterAlt0;
        characterAlts[1] = characterAlt1;
        characterAlts[2] = characterAlt2;
        characterImage.sprite = characterAlts[currentAltIndex];
    }

    public void UpdateAlt() {
        characterImage.sprite = characterAlts[currentAltIndex];
    }
}
