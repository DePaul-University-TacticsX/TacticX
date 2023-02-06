using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Warrior : CharacterEntity
{

    public Warrior(float speed) : base(speed, new Component()) {}

    public override void Attack()
    {
        Debug.Log("Warrior Attack!");
    }
}
