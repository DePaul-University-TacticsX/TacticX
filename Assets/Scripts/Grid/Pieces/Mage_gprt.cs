using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;

public class Mage_gprt : GamePiece
{
    public Mage_gprt(GameObject prefab)
        : base(prefab)
    {

    }

    public override void DoAction()
    {
        Debug.Log("Mage Do some action");
    }
}
