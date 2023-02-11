using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

namespace Michsky.UI.Freebie
{
    public class CharacterSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("CONTENT")]
        public Sprite previewIcon0;
        public Sprite previewIcon1;
        public Sprite previewIcon2;
        [HideInInspector] public Sprite characterIcon;
        [HideInInspector] public bool isDesignatedForSelection = false;
        public string characterName = "Character";
        public string characterType = "Support";
        [TextArea] public string characterInfo = "Character info here.";
        [TextArea] public string firstAbility = "First ability description here.";
        [TextArea] public string secondAbility = "Second ability description here.";
        [TextArea] public string thirdAbility = "Third ability description here.";

        [Header("SOUND")]
        public bool enableButtonSounds = false;
        public bool useHoverSound = true;
        public bool useClickSound = true;
        public bool useSelectSound = true;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        public AudioClip selectSound;
        public AudioSource soundSource;

        [Header("RESOURCES")]
        public Animator objectAnimator;
        public CharacterSelectManager characterManager;
        public Image previewImage;
        public Image characterImage;
        public TextMeshProUGUI characterText;

        [Header("SETTINGS")]
        public bool useCustomContent = false;

        [Header("EVENTS")]
        public UnityEvent onCharacterClick;
        public UnityEvent onCharacterSelection;

        private Sprite[] previewIcons = new Sprite[3];
        public int currentIconIndex = 0;

        void Start()
        {
            //TODO possibly load player data here?

            previewIcons[0] = previewIcon0;
            previewIcons[1] = previewIcon1;
            previewIcons[2] = previewIcon2;
            characterIcon = previewIcons[currentIconIndex];
            characterImage.sprite = previewIcons[currentIconIndex];

            if (useCustomContent == false)
                UpdateUI();
        }

        public void UpdateUI()
        {
            characterText.text = characterName;
            previewImage.sprite = previewIcons[currentIconIndex];
        }

        public void PrevCharacter() {
            if (isDesignatedForSelection) {
                if (currentIconIndex == 0)
                    currentIconIndex = 2;
                else {
                    currentIconIndex--;
                }

                Debug.Log(characterName + " prev to " + currentIconIndex);

                previewImage.sprite = previewIcons[currentIconIndex];
                characterImage.sprite = previewIcons[currentIconIndex];
            }
        }

        public void NextCharacter() {
            if (isDesignatedForSelection) {
                if (currentIconIndex == 2)
                    currentIconIndex = 0;
                else {
                    currentIconIndex++;
                }

                Debug.Log(characterName + " next to " + currentIconIndex);

                previewImage.sprite = previewIcons[currentIconIndex];
                characterImage.sprite = previewIcons[currentIconIndex];
            }
        }

        public void SelectCharacter()
        {
            objectAnimator.Play("Pressed to Selected");
            onCharacterSelection.Invoke();

            if (enableButtonSounds == true && useSelectSound == true)
                soundSource.PlayOneShot(selectSound);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableButtonSounds == true && useHoverSound == true)
                soundSource.PlayOneShot(hoverSound);

            if (!objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed") &&
                !objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pressed to Selected"))
                objectAnimator.Play("Normal to Hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed") &&
                !objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pressed to Selected"))
                objectAnimator.Play("Hover to Normal");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isDesignatedForSelection = !isDesignatedForSelection;
            if (isDesignatedForSelection)
                Debug.Log(characterName + "is designated for selection");

            if (enableButtonSounds == true && useClickSound == true)
                soundSource.PlayOneShot(clickSound);

            if (!objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Pressed to Selected") &&
                characterManager.enableSelecting == true)
            {
                onCharacterClick.Invoke();
                objectAnimator.Play("Hover to Pressed");
            }

            if (characterManager != null)
            {
                if (characterManager.currentObjectAnimator != null)
                    if (characterManager.currentObjectAnimator != objectAnimator)
                        characterManager.UpdateCharacter();

                characterManager.currentObjectAnimator = objectAnimator;
                characterManager.UpdateInfo();
            }
        }
    }
}