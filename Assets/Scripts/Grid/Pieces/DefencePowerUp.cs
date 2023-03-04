using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class DefencePowerUp : GamePiece
{
    public DefencePowerUp(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("DefencePowerUp Action");
        _ = Object.Instantiate(Resources.Load("Minigame menu prefab")) as GameObject;
        Object.FindObjectOfType<MiniGameManager>().PowerUpType = PowerUpType.Defence;
        TacticsX.GridImplementation.Grid.RemoveGamePiece(this, false);
    }

}
