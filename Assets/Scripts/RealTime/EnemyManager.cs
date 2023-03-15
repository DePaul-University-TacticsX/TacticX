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
    Enemy en1 = new Enemy(4.0f, 0.325f, new Vector3(-10, height, 0), ai1);
    enemy_list.Add(en1);

    EnemyAI ai2 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Demon"));
    Enemy en2 =  new Enemy(4.0f, 0.325f, new Vector3(0, height, 15), ai2);
    enemy_list.Add(en2);

    EnemyAI ai3 = Object.Instantiate<EnemyAI>(Resources.Load<EnemyAI>("Prefabs/Barbarian"));
    Enemy en3 = new Enemy(2.0f, 0.75f, new Vector3(0, height, -12), ai3);
    enemy_list.Add(en3);
  }

  public static List<Enemy> GetEnemies() {
    return enemy_list;
  }

  public static void CheckEndScene() {
    if (enemy_list.Count == 0) {
      enemy_list.Clear();
      BattleManager.CompleteBattle(BattleManager.GetPlayer2());
      //TacticsXGameManager.GetScenes().NextScene(Scenes.GridDemo);
    }
  }

  public static void AttackEnemy(Transform player) {
    
    List<Enemy> elist = EnemyManager.GetEnemies();
    List<Enemy> death_list = new List<Enemy>();
    foreach (Enemy enemy in elist) {
      float dist = Vector3.Distance(enemy.GetTransform().position, player.transform.position);
      
      // this is like the range of the player
      // it needs decoupled from enemy manager and set into the character
      if (dist < 2.5) {
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
