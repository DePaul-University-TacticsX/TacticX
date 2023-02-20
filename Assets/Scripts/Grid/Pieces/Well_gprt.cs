using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;


public class Well_gpenv : GamePiece
{


    public Well_gpenv(GameObject prefab) 
        : base(prefab) 
    {

    }

    public override void DoAction()
    {
        Debug.Log("Well Do some action");
    }
}

