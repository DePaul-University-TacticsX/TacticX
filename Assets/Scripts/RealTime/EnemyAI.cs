using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Looking { 
  LOOKING,
  NOT_LOOKING
}

public enum Moving {
  MOVING,
  NOT_MOVING
}

// Only see in intervals, while Looking, compute the rays, and move while not looking
// always move up until in range, then attack

public class EnemyAI : MonoBehaviour {
  

  // this enemy comes from the EnemyManager 
  // the Enemy manager resets this for the creation of a new EmemyAI 
  // these stats are copied locally into "this" enemy
  public Enemy enemy;

  // enemy stats, grabbed from Enemy
  public float speed;  
  public float melee_range;

  // static scene stats 
  static float deltime = 0.0f;     // set to Time.smoothDeltaTime later
  static float lookDelay = 40;
  
  // instance enemy stats
  float radius;   // radius of the raycast sphere
  int attackDelay;     // used to determine how many frames to pass before attacking
  float min_dist;    // some small length to stay back from the player
  
  // used to change dirsection when reaching plane bounds
  public int h_factor = 1;   
  public int w_factor = 1;

  // instance variables for player/target info 
  Vector3 seen;      // position of the "seen" target, when spotted
  public float target_distance;  

  // look to calculate where to move, don't look while moving
  public Looking isLooking;  


  public void SetEnemy(Enemy _enemy) {
    this.enemy = _enemy;
  }

  void Start() {    

    // bounds on sight, closeness, and melee range
    this.radius = 2.0f;    
    this.attackDelay = 80 * 2;  
    this.min_dist = 0.30f;  

    // initialize what you see to be only yourself at first
    this.seen = this.transform.position;

    // assume the distance to player is way too far way
    this.target_distance = float.MaxValue;

    // start as not looking
    this.isLooking = Looking.NOT_LOOKING;    

    // on instantiate objs during runtime, this is called after start
    // initialize this with specific Enemy stats
    this.speed = enemy.GetSpeed();
    this.melee_range = enemy.GetRange();
    transform.position = enemy.GetPosition(); 

  }
  
  void Awake() {   
 
  }

  void Update() {

    // adds a little delay, more of a hack for now
    flip_looking_on_with_delay();

    if (this.isLooking == Looking.LOOKING) {

      this.decide_target();

    }
    else {

      // move closer to player, if they are not close yet
      Vector3 TTP = this.transform.position;  
      if (not_close_yet()) {
        transform.Translate(speed*deltime*Math.Sign(seen.x - TTP.x), 0, speed*deltime*Math.Sign(seen.z - TTP.z));
      }
    }
    
  }

  void decide_target() {


    // check ahead
    Ray ahead_ray = new Ray(transform.position, transform.forward);
    bool see_target_ahead = SeeAndCast(ahead_ray);

    // check behind
    Vector3 behindDirection = new Vector3(ahead_ray.direction.x, ahead_ray.direction.y, ahead_ray.direction.z);
    behindDirection.z = -1 * behindDirection.z;    // flip to look behind
    Ray behind_ray = new Ray(transform.position, behindDirection);
    bool see_target_behind = SeeAndCast(behind_ray);

    // check right
    Ray right_ray = new Ray(transform.position, transform.right);
    bool see_target_right = SeeAndCast(right_ray);

    // check left
    Vector3 leftDirection = new Vector3(right_ray.direction.x, right_ray.direction.y, right_ray.direction.z); 
    leftDirection.x *= -1;
    Ray left_ray = new Ray(transform.position, leftDirection);
    bool see_target_left = SeeAndCast(left_ray);

    // check upper right
    Vector3 upperrightDir = new Vector3(right_ray.direction.x, right_ray.direction.y, ahead_ray.direction.z); 
    Ray upper_right_ray = new Ray(transform.position, upperrightDir);
    bool see_target_upper_right = SeeAndCast(upper_right_ray);

    // check upper left
    Vector3 upperleftDir = new Vector3(left_ray.direction.x, left_ray.direction.y, ahead_ray.direction.z); 
    Ray upper_left_ray = new Ray(transform.position, upperleftDir);
    bool see_target_upper_left = SeeAndCast(upper_left_ray);

    // check lower right
    Vector3 lowerrightDir = new Vector3(right_ray.direction.x, right_ray.direction.y, behind_ray.direction.z); 
    Ray lower_right_ray = new Ray(transform.position, lowerrightDir);
    bool see_target_lower_right = SeeAndCast(lower_right_ray);

    // check lower left
    Vector3 lowerleftDir = new Vector3(left_ray.direction.x, left_ray.direction.y, behind_ray.direction.z); 
    Ray lower_left_ray = new Ray(transform.position, lowerleftDir);
    bool see_target_lower_left = SeeAndCast(lower_left_ray);



    // makes the choice and sets the stats correctly
    if (see_target_ahead == true) {
      SeeAndCast(ahead_ray);   // actually set the target stats needed
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_behind == true) {
      SeeAndCast(behind_ray);   
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_right == true) {
      SeeAndCast(right_ray);   // actually set the target stats needed
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_left == true) {
      SeeAndCast(left_ray);   
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_upper_right == true) {
      SeeAndCast(upper_right_ray);   // actually set the target stats needed
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_upper_left == true) {
      SeeAndCast(upper_left_ray);   
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_lower_right == true) {
      SeeAndCast(lower_right_ray);   // actually set the target stats needed
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else if (see_target_lower_left == true) {
      SeeAndCast(lower_left_ray);   
      this.isLooking = Looking.NOT_LOOKING;
      this.canAttack();
    }
    else {
      // this is when the enemy does not know what to do
      // sort of like idle moving

      this.isLooking = Looking.LOOKING;
      
      //  X-Z plane  
      //  top left corner: (-20, 25)
      //  bottom right corner: (20, -25)  
      // transform.Translate(0, 0, speed*deltime*Math.Sign(transform.position.z));
      
      float myz = this.transform.position.z;
      float myx = this.transform.position.x;

      // force it the enemy bounce up down off the z bounds of the plane
      if (myz > 25) {
        this.h_factor = -1;
      }

      else if (myz < -25) {
        this.h_factor = 1;
      }

      // force a bounce off the x bounds of the plane
      if (myx < -20) {
        this.w_factor = 1;
      }
      else if (myx > 20) {
        this.w_factor = -1;
      }


      // move diagonally, but half as much in the x-direction
      transform.Translate(0.5f*w_factor*speed*deltime, 0, h_factor*speed*deltime);
    
    }

   

  }

  void canAttack() {
    if ((Time.frameCount % attackDelay == 0) && IsInRange()) {
      AttackPlayer(2);
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
    if (Time.frameCount % lookDelay == 0) {
      this.isLooking = Looking.LOOKING;
    }
  }

  bool SeeAndCast(Ray ray) {
    
    RaycastHit hit;

    // SphereCast: casts a sphere of radius, along the ray, returns info on what was hit
    // if an object is seen and it is a player
    bool isSeen = Physics.SphereCast(ray, this.radius, out hit);  
    if (isSeen) {
      this.seen = hit.transform.position;
      deltime = Time.smoothDeltaTime;    // approximatly 0.01 on my machine
      this.target_distance = hit.distance;
    }
    return isSeen;
  }

  // for debugging
  static void Check(System.Object obj, int sec, string label) {
    if (sec == 0) {
      Debug.Log($"{label}:\n{obj}");  
    }
    else if (Time.frameCount % (sec * 80) == 0) {
      Debug.Log($"{label}:\n{obj}");     
    }
  } 


  public static void AttackPlayer(int amount) {
    // Debug.Log($"attack of {amount}");
    RTManager.DecreaseHealth(RTManager.getActive(), amount);
    
  } 
  

}