using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class

using System;   // for System.InvalidOperationException

// needs namespace?

public class MySceneManager : MonoBehaviour, IManager {

  public ManagerStatus MStatus;
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
    this.MStatus = ManagerStatus.ON;
    this.CurrentScene = Scenes.Scene1;
    this.EndScene = Scenes.Scene3;
    this.LStatus = LoadStatus.COMPLETE;

  }

  public void NextScene() {  

    // shift to the next scene in the iterator and hold on to it
    this.SceneIter.MoveNext();
    
    // try to grab the current Scene from iterator
    Scenes next;
    try {
      next = (Scenes) this.SceneIter.Current;
    }
    catch(InvalidOperationException e) { 
      Debug.Log($"{e.Message}: Next Scene is not available");
      return;
    }
    
    // set the load status
    this.LStatus = LoadStatus.LOADING;

    Debug.Log($"Loading {next} ... ");
    
    // set the current scene
    this.CurrentScene = next;

    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;
     Debug.Log($"Loading {next} is complete ... ");

  }

  public bool isLoadingComplete() {
    if (this.LStatus == LoadStatus.COMPLETE) {
      return true;
    }
    return false;
  }
  
}

