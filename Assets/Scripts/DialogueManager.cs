using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.CullingGroup;
using System;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public TMP_Text buttonText;
    public static DialogueManager manager;
    public DialogueState State;
    public static event Action<DialogueState> OnStateChanged;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public bool isActive = false;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            isActive = true;
            //DontDestroyOnLoad(this);
        }
        else if (manager == this)
        {
            Destroy(gameObject);
        }

    }

    public void UpdateDialogueState(DialogueState newState)
    {
        State = newState;

        switch (newState)
        {
            case DialogueState.Win:
                Instantiate(Resources.Load<GameObject>("WinDialogue"));
                break;
            case DialogueState.Lose:
                Instantiate(Resources.Load<GameObject>("LoseDialogue"));
                break;
            case DialogueState.Start:
                Instantiate(Resources.Load<GameObject>("StartDialogue"));
                FindObjectOfType<DialogueTrigger>().StartDialogue();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnStateChanged?.Invoke(newState);
    }
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        buttonText.text = "Next Message";

        Debug.Log("Started Dialogue. Loaded messages: " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage == currentMessages.Length-1)
        {
            buttonText.text = "End";
        }
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Dialogue ended.");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            Destroy(transform.gameObject.GetComponentInParent<Canvas>().gameObject);
            if (State == DialogueState.Win)
            {
                Instantiate(Resources.Load<GameObject>("WinMenu"));
            }
            else if (State == DialogueState.Lose)
            {
                Instantiate(Resources.Load<GameObject>("LoseMenu"));
            }
        }
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    private void Update()
    {
    }

    private void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }
}

public enum DialogueState
{
    Start,
    Win,
    Lose
}