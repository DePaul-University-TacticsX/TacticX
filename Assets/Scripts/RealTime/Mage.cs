using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mage : CharacterEntity
{

    public Mage(float speed) : base(speed, "Mage") { }

    public override void Attack()
    {
        Debug.Log("Mage Attack!");
    }
}