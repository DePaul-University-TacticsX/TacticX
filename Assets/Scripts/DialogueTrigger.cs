using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages,actors);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && FindObjectOfType<DialogueManager>().isActive == false)
        {
            StartDialogue();
        }
    }
}

[Serializable]
public class Message
{
    public int actorID;
    public string message;
}

[Serializable]
public class Actor
{
    public string name; 
    public Sprite sprite;
}


