using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : CharacterEntity
{

    public Knight(float speed) : base(speed, "Knight") { }

    public override void Attack()
    {
        Debug.Log("Knight Attack!");
    }
}