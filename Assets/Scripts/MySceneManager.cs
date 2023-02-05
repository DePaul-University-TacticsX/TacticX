using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class
// needs namespace?

public class MySceneManager : MonoBehaviour, IManager {

  public ManagerStatus status;
  public Scenes CurrentScene;
  public Scenes EndScene;
  public LoadStatus LStatus;

  IEnumerator SceneIter;


  public void StartUp() {
    Debug.Log("Scene Manager is starting at Scene 1 ... ");

    // GManager is a gameObject in Scene1, so by default the game starts in scene1
    // rough collection of scenes, may need to be a List later
    Scenes[] SceneArray = {Scenes.Scene2, Scenes.Scene3};
    
    // grab the iterator
    this.SceneIter = SceneArray.GetEnumerator(); 
    
    // set the statuses
    this.CurrentScene = Scenes.Scene1;
    this.EndScene = Scenes.Scene3;
    this.status = ManagerStatus.ON;
    this.LStatus = LoadStatus.COMPLETE;

  }

  public ManagerStatus GetStatus() {
    return this.status;
  }

  public LoadStatus GetLoadStatus() {
    return this.LStatus;
  }

  public void NextScene() {

    // do nothing if at the last scene
    if ((Scenes) this.CurrentScene == this.EndScene) {
      return; 
    }  

    // set the load status
    this.LStatus = LoadStatus.LOADING;

    // shift to the next scene in the iterator and hold on to it
    this.SceneIter.MoveNext();
    Scenes next = (Scenes) this.SceneIter.Current;
    
    Debug.Log($"Loading {next} ... ");
    
    // set the current scene
    this.CurrentScene = next;

    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;

    

  }

  
}

