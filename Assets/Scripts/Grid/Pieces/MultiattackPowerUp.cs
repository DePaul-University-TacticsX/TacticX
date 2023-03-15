using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class MultiattackPowerUp : GamePiece
{
    public MultiattackPowerUp(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("MultiattackPowerUp Action");
        _ = Object.Instantiate(Resources.Load("Minigame menu prefab")) as GameObject;
        Object.FindObjectOfType<MiniGameManager>().PowerUpType = PowerUpType.MultiAttack;
        TacticsX.GridImplementation.Grid.RemoveGamePiece(this, false);
    }

}
  