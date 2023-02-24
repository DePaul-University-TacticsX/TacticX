using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour {
  public float speed = 2.0f;     // same as the Archer
  public float ObstacleRange = 100.0f;
  public float height = 2.0f;   // fix on the 2d plane
  public float radius = 4.0f;   // radius of the raycast sphere
  static int SEC = 10;
  public Vector3 seen;
  float deltime;
  static int SeenDelay = 60;    // in frames

  void Start() {
    transform.position = new Vector3(0, height, -25);
    Debug.Log($"Enemy starting pos: {this.transform.position}");
    this.seen = this.transform.position;
  }
  
  void Update() {

    // always move closer
    Vector3 TTP = this.transform.position;
    transform.Translate(speed*deltime*Math.Sign(seen.x - TTP.x), 0, speed*deltime*Math.Sign(seen.z - TTP.z));

    // adds a little delay, more of a hack for now
    if (Time.frameCount % SeenDelay == 0) {

      Ray ray = new Ray(transform.position, transform.forward);

      // check ahead
      SeeAndCast(ray);

      // check behind
      Vector3 behindDirection = ray.direction;
      behindDirection.z = -1 * behindDirection.z;    // flip to look behind
      SeeAndCast(new Ray(ray.origin, behindDirection));
    }
    
    if (Input.GetKey(KeyCode.Z)) {
      AttackPlayer(10);
    }
  
  }

  // SphereCast: casts a sphere of radius, along the ray, returns info on what was hit

  void SeeAndCast(Ray ray) {
    RaycastHit hit;
    if (Physics.SphereCast(ray, this.radius, out hit)) {
      this.seen = hit.transform.position;
      this.deltime = Time.deltaTime;
      Check(seen, SEC, "RaycastHit");
    }
  }

  static void Check(System.Object obj, int sec, string label) {
    if (sec == 0) {
      Debug.Log($"{label}:\n{obj}");  
    }
    else if (Time.frameCount % (sec * 80) == 0) {
      Debug.Log($"{label}:\n{obj}");     
    }
  } 

  public static void AttackPlayer(int amount) {
    RTManager.DecreaseHealth(RTManager.getActive(), amount);
  } 



}