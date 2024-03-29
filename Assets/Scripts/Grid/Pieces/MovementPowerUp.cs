using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class MovementPowerUp : GamePiece
{
    public MovementPowerUp(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("MovementPowerUp Action");
        _ = Object.Instantiate(Resources.Load("Minigame menu prefab")) as GameObject;
        Object.FindObjectOfType<MiniGameManager>().PowerUpType = PowerUpType.Movement;
        TacticsX.GridImplementation.Grid.RemoveGamePiece(this, false);
    }

}
