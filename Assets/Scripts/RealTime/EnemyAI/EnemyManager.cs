using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  private List<Enemy> enemy_list;
  static float height = 2.0f;   // fix on the 2d plane

  void Start() {
    
    // speed (in between warrior and archer), range, position
    
    // EnemyAI.SetEnemy(new Enemy(3.5f, 0.3f, new Vector3(-5, height, 0)));
    // EnemyAI en1 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Demon"));
    
    // // this demon is slightly slower
    // EnemyAI.SetEnemy(new Enemy(3.0f, 0.3f, new Vector3(5, height, -20)));
    // EnemyAI en2 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Demon"));
    
    // barbarian is much slower
    EnemyAI.SetEnemy(new Enemy(0.75f, 0.3f, new Vector3(-5, height, -20)));
    EnemyAI en3 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Barbarian"));
  }

  void Update() {
  
  }
}

