using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class Dialogue : GamePiece
{
    public DialogueTrigger trigger = new DialogueTrigger();
    public DialogueManager manager = new DialogueManager();

    public Dialogue(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("Dialogue Action");
        trigger.StartDialogue();
    }

}
