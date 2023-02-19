using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI: MonoBehaviour {
  public float speed = 0.5f;
  public float ObstacleRange = 5.0f;
  public float height = 7.5f;   // fix on the 2d plane
  public float radius = 3;


  void Start() {
    transform.position = new Vector3(11, height, -16);
  }
  
  void Update() {

    // creates a ray starting at the enemies position, along the z axis
    Ray ray = new Ray(transform.position, transform.forward);
    
    RaycastHit hit;

    // SphereCast: casts a sphere of radius, along the ray, returns info on what was hit
    if (Physics.SphereCast(ray, this.radius, out hit)) {
      
      if (hit.distance < ObstacleRange) {
        
        // Rotates the object (does not move)
        // float angle = Random.Range(-110, 110);
        // transform.Rotate(0, angle, 0);

        // move to a random position
        transform.Translate(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f), this.transform);
      }
    }
  }
}