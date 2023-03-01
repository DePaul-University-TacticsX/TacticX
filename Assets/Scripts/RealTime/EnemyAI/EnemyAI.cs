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
  
  // the enemy this at instance is controlling
  // some Enemy or derived from Enemy
  public static Enemy enemy;

  // enemy stats, grabbed from Enemy
  public float speed;  
  float melee_range;

  // static scene stats 
  static float deltime;     // Time.delTime is about 0.01f

  
  // instance scene stats
  float radius;   // radius of the raycast sphere
  int attackDelay;     // used to determine how many frames to pass before attacking
  float min_dist;    // some small length to stay back from the player

  
  // instance variables for player/target info 
  Vector3 seen;      // position of the "seen" target, when spotted
  float target_distance;  // distance when started

  // look to calculate where to move, don't look while moving
  public Looking isLooking;  
  
  // useful counts
  public int attack_count;


  public static void SetEnemy(Enemy _enemy) {
    enemy = _enemy;
  }

  void Start() {
    // bounds on sight and melee range
    this.min_dist = 0.15f;    // needs to be less than melee_range
    this.radius = 3.0f;    // radius of the raycast

    // initialize what you see to be only yourself at first
    this.seen = this.transform.position;

    // assume the distance is way too far way
    this.target_distance = float.MaxValue;

    // immediately start looking
    this.isLooking = Looking.LOOKING;

    // static stats
    deltime = 0f;       // used later
    attackDelay = 80;   // in frames  
    
  }
  
  void Awake() {   // on instantiate objs during runtime, this is called after start
    // initialize this specific Enemy stats
    this.speed = enemy.GetSpeed();
    this.melee_range = enemy.GetRange();
    transform.position = enemy.GetPosition();  
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

      this.isLooking= Looking.NOT_LOOKING;
    }

    if ((this.isLooking == Looking.NOT_LOOKING)) {
      Vector3 TTP = this.transform.position;
      
      // always move closer, but only so close, while it is not melee range
      if (not_close_yet()) {
        transform.Translate(speed*deltime*Math.Sign(seen.x - TTP.x), 0, speed*deltime*Math.Sign(seen.z - TTP.z));
      }
      // for when player is able to reach inside the minimum distance bound, move away
      // this could get removed, once the left and right rays are added, and there is less of gap in the 2D vision
      else {
        // move away at a quarter the enemy's speed
        transform.Translate(0.25f*speed*deltime*Math.Sign(seen.x - TTP.x), 0, -0.25f*speed*deltime*Math.Sign(seen.z - TTP.z));
      }

      // flip to not moving on a "delay" if needed
      flip_looking_on_with_delay();
    }

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
    // if an object is seen and it is a player
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
    Debug.Log($"attack of {amount}");
    // RTManager.DecreaseHealth(RTManager.getActive(), amount);

  } 

  
  

}