using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class

public class MySceneManager : MonoBehaviour, IManager {

  public ManagerStatus status;
  
  public Scene CurrentScene;
  public Scene EndScene;
  public IEnumerator SceneIter;

  public void StartUp() {
    Debug.Log("Scene Manager is starting at Scene 1 ... ");

    // GManager is a gameObject in Scene1, so by default the game starts in scene1
    Scene[] SceneArray = {Scene.Scene2, Scene.Scene3};
    
    this.SceneIter = SceneArray.GetEnumerator(); 
    
    this.CurrentScene = Scene.Scene1;
    this.EndScene = Scene.Scene3;
    this.status = ManagerStatus.ON;

  }

  public ManagerStatus GetStatus() {
    return this.status;
  }

  public void NextScene() {
    if ((Scene) this.CurrentScene == this.EndScene) {
      return; 
    }  
    this.SceneIter.MoveNext();
    Scene next = (Scene) this.SceneIter.Current;
    Debug.Log($"Loading {next} ... ");
    this.CurrentScene = next;
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run
  }


}