using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy  {
  private float speed;     // speed of enemy
  private float range;     // range of attack  
  private Vector3 position;
  
  public Enemy(float speed, float range, Vector3 position) {
    this.speed = speed;
    this.range = range;
    this.position = position;
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
}