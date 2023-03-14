using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
  private static List<Enemy> enemy_list;
  static float height = 2.0f;   // fix on the 2d plane

  void Start() {
    
    enemy_list = new List<Enemy>();

    // speed, range, position
    
    EnemyAI ai1 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Demon"));
    Enemy en1 = new Enemy(3.5f, 0.3f, new Vector3(-5, height, 5), ai1);
    enemy_list.Add(en1);
    

    //  this demon is slightly slower
    EnemyAI ai2 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Demon"));
    Enemy en2 =  new Enemy(3.0f, 0.3f, new Vector3(5, height, -5), ai2);
    enemy_list.Add(en2);

    // barbarian is much slower
    EnemyAI ai3 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Barbarian"));
    Enemy en3 = new Enemy(1.5f, 0.75f, new Vector3(-5, height, -5), ai3);
    enemy_list.Add(en3);
  }

  public static List<Enemy> GetEnemies() {
    return enemy_list;
  }

  public static void CheckEndScene() {
    if (enemy_list.Count == 0) {
      enemy_list.Clear();
      TacticsXGameManager.GetScenes().NextScene(Scenes.GridDemo);
    }
  }

  public static void AttackEnemy(Transform player) {
    
    List<Enemy> elist = EnemyManager.GetEnemies();
    List<Enemy> death_list = new List<Enemy>();
    foreach (Enemy enemy in elist) {
      float dist = Vector3.Distance(enemy.GetTransform().position, player.transform.position);
      if (dist < 2) {
        Alive status = enemy.take_damage(5);
        if (status == Alive.NO) {
          death_list.Add(enemy);
        }
      }
    }

    foreach (Enemy dead in death_list) {
      EnemyManager.GetEnemies().Remove(dead);
    }

    EnemyManager.CheckEndScene();

  }

  void Update() {
  
  }
}
