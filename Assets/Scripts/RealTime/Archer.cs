using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterEntity
{

    public Archer(float speed) : base(speed, "Archer") { }

    public override void Attack()
    {
        Debug.Log("Archer Attack!");
    }
}
