using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;

public class Barbarian_gprt : GamePiece
{
    
    public Barbarian_gprt(GameObject prefab)
        : base(prefab)
    {

    }

    public override void DoAction()
    {
        BattleManager.StartBattle(TurnManager.GetCurrentTurn(), TurnManager.FindParticipant(this));        
    }
}
