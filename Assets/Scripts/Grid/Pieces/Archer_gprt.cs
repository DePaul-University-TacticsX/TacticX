using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;

public class Archer_gprt : GamePiece
{
    public Archer_gprt(GameObject prefab)
        : base(prefab)
    {

    }

    public override void DoAction()
    {
        BattleManager.StartBattle(TurnManager.GetCurrentTurn(), TurnManager.FindParticipant(this));
    }
}
