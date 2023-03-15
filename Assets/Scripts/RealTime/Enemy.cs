using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy  {
  private float speed;     // speed of enemy
  private float range;     // range of attack  
  private Vector3 position;
  private int health;
  private EnemyAI ai;

  public Enemy(float speed, float range, Vector3 position, EnemyAI _ai) {
    this.speed = speed;
    this.range = range;
    this.position = position;
    this.health = 15;
    this.ai = _ai;
    this.ai.SetEnemy(this);
  }
  public float GetSpeed() {
    return this.speed;
  }

  public float GetRange() {
    return this.range;
  }

  public Vector3 GetPosition() {
    return this.position;
  }

  public void DecreaseHealth(int num) {
    this.health -= num;
  }

  public Transform GetTransform() {
    return this.ai.transform;
  }

  public Alive take_damage(int num) {
    if (this.health > 5) {
      this.health -= num;
      Debug.Log($"SCREAM! Enemy Hit");
      this.ai.getScream().Play();
      return Alive.YES;
    }
    else if (this.health == 5 ) {
      this.ai.gameObject.SetActive(false);
      return Alive.NO;
    }
    else {
      return Alive.YES;
    }
  }
}

public enum Alive {
  YES,
  NO
}