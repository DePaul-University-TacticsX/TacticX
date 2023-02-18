using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;


public class Well : GamePiece
{


    public Well(GameObject prefab) 
        : base(prefab) 
    {

    }

    public override void DoAction()
    {
        Debug.Log("Do some action");
    }
}

