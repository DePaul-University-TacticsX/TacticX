using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class CharacterEntity
{

    public float direction = 1.0f;
    public float speed;
    public Component entity;

    public CharacterEntity(float speed, Component c) {

        this.speed = speed;
        this.entity = c;
    }

    public void Move()
    {
        entity.transform.Translate(new Vector3(0, 0, this.direction * Time.deltaTime * this.speed)); // Slow
    }

    public abstract void Attack();

    public void changeDirection() {
        this.direction = this.direction * -1;
    }

}
