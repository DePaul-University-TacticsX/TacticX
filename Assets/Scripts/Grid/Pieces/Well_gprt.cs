using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TacticsX.GridImplementation;


public class Well_gprt : GamePiece
{


    public Well_gprt(GameObject prefab) 
        : base(prefab) 
    {

    }

    public override void DoAction()
    {
        Debug.Log("Well Do some action");
    }
}

