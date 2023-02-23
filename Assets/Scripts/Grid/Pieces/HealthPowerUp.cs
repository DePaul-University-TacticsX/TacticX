using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class HealthPowerUp : GamePiece
{
    public HealthPowerUp(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("HealthPowerUp Action");

        _ = Object.Instantiate(Resources.Load("Minigame menu prefab")) as GameObject;
    }

}
