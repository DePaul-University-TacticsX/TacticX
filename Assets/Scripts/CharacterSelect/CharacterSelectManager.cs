using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TacticsX.Data;

namespace TacticsX.TeamBuilder
{
    public class CharacterSelectManager : MonoBehaviour
    {
        [Header("CONTENT")]
        public string selectFirstLine;
        public string selectSecondLine;
        public Sprite selectCharaterIcon;

        [Header("RESOURCES")]
        public Animator characterWindow;
        public Image characterImage;
        public TextMeshProUGUI characterNameText;
        public TextMeshProUGUI characterNameHelperText;
        public TextMeshProUGUI characterTypeText;
        public TextMeshProUGUI characterTypeHelperText;
        public TextMeshProUGUI characterBioText;
        public TextMeshProUGUI firstAbilityText;
        public TextMeshProUGUI secondAbilityText;
        public TextMeshProUGUI thirdAbilityText;

        [Header("SETTINGS")]
        public float selectionCooldown = 1;

        [HideInInspector] public Animator currentObjectAnimator;
        [HideInInspector] public CharacterSelectButton currentCharacter;
        [HideInInspector] public string currentName;
        [HideInInspector] public string currentType;
        [HideInInspector] public bool enableSelecting = true;
        [HideInInspector] private const int countCharacterSelectButtons = 3;
        [HideInInspector] public CharacterSelectButton[] team = new CharacterSelectButton[countCharacterSelectButtons];

        [HideInInspector] public bool isInitialized { get; private set;}

        //TODO add a section for character select buttons to add to team list

        void Start()
        {
            characterTypeText.text = selectFirstLine;
            characterNameText.text = selectSecondLine;
            characterTypeHelperText.text = selectFirstLine;
            characterNameHelperText.text = selectSecondLine;
            characterBioText.text = "";
            currentType = selectFirstLine;
            currentName = selectSecondLine;
            characterImage.sprite = selectCharaterIcon;

            GetTeam();
            LoadTeam();

            isInitialized = true;
        }

        private void GetTeam() {
            for (int i = 0; i < countCharacterSelectButtons; i++) {
                team[i] = transform.GetChild(4).GetChild(i).GetComponent<CharacterSelectButton>();
            }
        }

        public void SaveTeam() {
            SaveManager.SaveTeamData(team);
        }

        public void LoadTeam() {
            TeamData teamData = SaveManager.LoadTeamData();

            if (teamData != null) {
                for (int i = 0; i < team.Length; i++) {
                    team[i].currentIconIndex = teamData.characterAlts[i];
                    team[i].characterName = teamData.characterNames[i];
                    team[i].characterText.text = team[i].characterName;
                    team[i].UpdateIcons();
                }
            }
        }

        public void UpdateCharacter()
        {
            if (currentObjectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed"))
                currentObjectAnimator.Play("Pressed to Normal");
            else if (currentObjectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pressed to Selected"))
                currentObjectAnimator.Play("Selected to Normal");
        }

        public void UpdateInfo()
        {
            if (enableSelecting == true)
            {
                StopCoroutine("CalculateName");
                StopCoroutine("CalculateType");
                StopCoroutine("ChangeBio");

                if (currentCharacter == null)
                    characterWindow.Play("Change First");
                else
                    characterWindow.Play("Change");

                if (currentObjectAnimator != null)
                {
                    currentCharacter = currentObjectAnimator.gameObject.GetComponent<CharacterSelectButton>();

                    if (currentCharacter.characterName.Length <= currentName.Length)
                    {
                        characterNameHelperText.text = currentName;
                        StartCoroutine("CalculateName");
                    }

                    else if (currentCharacter.characterName.Length >= currentName.Length)
                    {
                        characterNameText.text = currentCharacter.characterName;
                        characterNameHelperText.text = currentName;
                        currentName = currentCharacter.characterName;
                    }

                    if (currentCharacter.characterType.Length <= currentType.Length)
                    {
                        characterTypeHelperText.text = currentType;
                        StartCoroutine("CalculateType");
                    }

                    else if (currentCharacter.characterType.Length >= currentType.Length)
                    {
                        characterTypeText.text = currentCharacter.characterType;
                        characterTypeHelperText.text = currentType;
                        currentType = currentCharacter.characterType;
                    }

                    if (firstAbilityText != null)
                        firstAbilityText.text = currentCharacter.firstAbility;

                    if (secondAbilityText != null)
                        secondAbilityText.text = currentCharacter.secondAbility;

                    if (thirdAbilityText != null)
                        thirdAbilityText.text = currentCharacter.thirdAbility;

                    characterBioText.text = currentCharacter.characterInfo;
                    StartCoroutine("ChangeCharacterIcon");
                    StartCoroutine("SelectionCooldown");
                    enableSelecting = false;
                }
            }
        }

        IEnumerator CalculateName()
        {
            yield return new WaitForSeconds(currentObjectAnimator.GetCurrentAnimatorStateInfo(0).length / 2);
            characterNameText.text = currentCharacter.characterName;
            currentName = currentCharacter.characterName;
            characterBioText.text = currentCharacter.characterInfo;
            StopCoroutine("CalculateName");
        }

        IEnumerator CalculateType()
        {
            yield return new WaitForSeconds(currentObjectAnimator.GetCurrentAnimatorStateInfo(0).length / 2);
            characterTypeText.text = currentCharacter.characterType;
            currentType = currentCharacter.characterType;
            StopCoroutine("CalculateType");
        }

        IEnumerator ChangeCharacterIcon()
        {
            yield return new WaitForSeconds(currentObjectAnimator.GetCurrentAnimatorStateInfo(0).length / 2);
            characterImage.sprite = currentCharacter.previewIcons[currentCharacter.currentIconIndex];
            StopCoroutine("ChangeCharacterIcon");
        }

        IEnumerator SelectionCooldown()
        {
            yield return new WaitForSeconds(selectionCooldown);
            enableSelecting = true;
            StopCoroutine("SelectionCooldown");
        }
    }
}