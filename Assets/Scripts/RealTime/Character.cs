using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterEntity
{

    public float direction = 1.0f;
    public float speed;
    public string prefab_name;
    public string name;
    public GameObject entity;
    public int health;
    public GameObject hurt;

    public CharacterEntity(float speed, string prefab_name) {
        this.speed = speed;
        this.prefab_name = prefab_name;
        this.entity = (GameObject) Object.Instantiate(Resources.Load(string.Format("Prefabs/{0}", prefab_name)));
        this.health = 10;
        this.name = "player";
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.S)) {
            // Going Left, Negative Z from Point-of-origin
            entity.transform.Translate(new Vector3(0, 0, -(Time.deltaTime * this.speed)));
        }

        if (Input.GetKey(KeyCode.D)) {
            // Going Down, Positive X Value from Point-of-origin
            entity.transform.Translate(new Vector3(Time.deltaTime * this.speed, 0, 0));
        }

        if (Input.GetKey(KeyCode.A)) {
            // Going Up, Negative X Value from Point-of-origin
            entity.transform.Translate(new Vector3(-(Time.deltaTime * this.speed), 0, 0));
        }

        if (Input.GetKey(KeyCode.W)) {
            // Going Right, Positive Z from Point-of-origin
            entity.transform.Translate(new Vector3(0, 0, Time.deltaTime * this.speed));
        }
    }

    public abstract void Attack();

    public Vector3 get_position() {

        return entity.transform.position;
    
    }

    public Quaternion get_rotation() {
        return entity.transform.rotation;
    }

    public void minus_health(int amount) {
      this.health -= amount;
      Debug.Log($"Ouch! Health at {this.health}");
    }

    public int get_health() {
      return this.health;
    }

}
