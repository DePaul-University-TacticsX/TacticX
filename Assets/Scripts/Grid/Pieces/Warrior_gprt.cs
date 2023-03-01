using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;

public class Warrior_gprt : GamePiece
{
    
    public Warrior_gprt(GameObject prefab)
        : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("Warrior Do some action");
        TacticsXGameManager.GetScenes().NextScene(Scenes.BattleFieldRealTime);
    }
}
