using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Looking { 
  LOOKING,
  NOT_LOOKING
}

// Only see in intervals, while Looking, compute the rays, and move while not looking
// always move up until in range, then attack

public class EnemyAI : MonoBehaviour {
  
  // scene stats
  public float height = 2.0f;   // fix on the 2d plane
  
  // enemy stats
  public float speed = 1.5f;     // same as the Archer
  public float radius = 1f;   // radius of the raycast sphere
  public float min_dist;    // some small length to stay back from the player
  
  // player info
  public Vector3 seen;      // position of the "seen" target, when spotted
  public float target_distance;  // distance when starteed

  // statics, may add to them
  static float deltime;     // Time.delTime is about 0.01f
  static int attackDelay;     // used to determine how many frames to pass before attacking

  // look to calculate where to move, don't look while moving
  public Looking isLooking;  
  
  // useful counts
  public int look_count;
  public int attack_count;


  float melee_range;

  void Start() {
    // static stats
    deltime = 0f;
    attackDelay = 80;   // in frames

    // initialize the start position and melee range
    transform.position = new Vector3(0, height, -25);
    this.melee_range = 0.5f;
    this.min_dist = 0.15f;    // needs to be less than range
  

    // initialize what you see to be only yourself at first
    this.seen = this.transform.position;

    // assume the distance is way too far way
    this.target_distance = float.MaxValue;

    // immediately start looking
    this.isLooking = Looking.LOOKING;

    this.attack_count = 0;

  }
  
  void Update() {

    // adds a little delay, more of a hack for now
    // if (Time.frameCount % SeenDelay == 0) {
    if (this.isLooking == Looking.LOOKING) {

      Ray ray = new Ray(transform.position, transform.forward);

      // check ahead
      bool see_target_ahead = SeeAndCast(ray);

      // check behind
      Vector3 behindDirection = ray.direction;
      behindDirection.z = -1 * behindDirection.z;    // flip to look behind
      bool see_target_behind = SeeAndCast(new Ray(ray.origin, behindDirection));

      // if you don't see player in either direction, then reset the distance
      if (see_target_ahead == false && see_target_behind == false) {
        this.target_distance = float.MaxValue;
      }

      ++look_count;
      this.isLooking= Looking.NOT_LOOKING;
    }

    // always move closer, but only so close, while it is not melee range
    if ((this.isLooking == Looking.NOT_LOOKING)) {
      Vector3 TTP = this.transform.position;
      
      if (not_close_yet()) {
        transform.Translate(speed*deltime*Math.Sign(seen.x - TTP.x), 0, speed*deltime*Math.Sign(seen.z - TTP.z));
      }

      // flip to not moving on a "delay" if needed
      flip_looking_on_with_delay();
    }

    // if (this.IsInRange()) {
    //   Debug.Log("in range");
    //   Debug.Log(++this.range_count);
    // }

    // if the enemy is in range and its time to attack
    canAttack();
  }

  void canAttack() {
    // made it depend only on the same delay as when they are moving for now
    if ((Time.frameCount % attackDelay == 0) && IsInRange()) {
      AttackPlayer(1);
      ++this.attack_count;
    }
  }

  bool not_close_yet() {
    if (this.min_dist <= this.target_distance) {
      return true;   // can proceed to move closer
    }
    else {
      return false;
    }

  }

  bool IsInRange() {
    if (this.melee_range > this.target_distance) {
      return true;
    }
    else {
      return false;
    }
  }

  void flip_looking_on_with_delay() {
    // if (Time.frameCount % moveDelay == 0) {
    //   this.isLooking = Looking.LOOKING;
    // }
    // always look for now
    this.isLooking = Looking.LOOKING;
  }

  bool SeeAndCast(Ray ray) {
    RaycastHit hit;
    // SphereCast: casts a sphere of radius, along the ray, returns info on what was hit
    bool isSeen = Physics.SphereCast(ray, this.radius, out hit); 
    if (isSeen) {
      this.seen = hit.transform.position;
      deltime = 0.01f;    // approximate Time.delTime
      this.target_distance = hit.distance;
      // Check(seen, 0, "RaycastHit");
    }
    return isSeen;
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
    Debug.Log($"attack player by {amount}");
    // RTManager.DecreaseHealth(RTManager.getActive(), amount);

  } 



}