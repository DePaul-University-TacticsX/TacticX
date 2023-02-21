using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI: MonoBehaviour {
  public float speed = 2.0f;     // same as the Archer
  public float ObstacleRange = 100.0f;
  public float height = 2.0f;   // fix on the 2d plane
  public float radius = 2.0f;
  static int SEC = 10;
  public Vector3 seen;
  public Vector3 prevseen;
  float deltime;
  static int SeenDelay = 60;    // in frames

  void Start() {
    transform.position = new Vector3(0, height, -25);
    Debug.Log($"Enemy starting pos: {this.transform.position}");
    this.seen = this.transform.position;
    this.prevseen = this.transform.position;
  }
  
  void Update() {

    // always make progress towards the last seen
    if (seen != prevseen) {
      Vector3 TTP = this.transform.position;
      transform.Translate(speed*deltime*Math.Sign(seen.x - TTP.x), 0, speed*deltime*Math.Sign(seen.z - TTP.z));
    }

    Ray ray = new Ray(transform.position, transform.forward);
    Check(ray, SEC, "Enemy Ray");

    // SphereCast: casts a sphere of radius, along the ray, returns info on what was hit
    // adds a little delay, more of a hack for now
    RaycastHit hit;
    if ((Time.frameCount % SeenDelay == 0) && Physics.SphereCast(ray, this.radius, out hit)) {
      this.prevseen = this.seen;
      
      this.seen = hit.transform.position;
      this.deltime = (float) Math.Round(Time.deltaTime, 2);
      Check(seen, 0, "RaycastHit");
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




}