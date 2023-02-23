using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;

public class DamagePowerUp : GamePiece
{
    public DamagePowerUp(GameObject prefab) : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("DamagePowerUp Action");
        _ = Object.Instantiate(Resources.Load("Minigame menu prefab")) as GameObject;
    }

}
