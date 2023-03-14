using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : CharacterEntity
{

    public Warrior(float speed) : base(speed, "Knight") {}

    public override void Attack()
    {
        Debug.Log("Knight Attack!");
    }
}
