using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;

public class Knight_gprt : GamePiece
{
    public Knight_gprt(GameObject prefab)
        : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("Knight Do some action");
        BattleManager.StartBattle(TurnManager.GetCurrentTurn(), TurnManager.FindParticipant(this));
    }
}
