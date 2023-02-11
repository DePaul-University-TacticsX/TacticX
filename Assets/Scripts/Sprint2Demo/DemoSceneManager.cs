using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class
using System;   // for System.InvalidOperationException

// needs namespace?

public class DemoSceneManager : MonoBehaviour, IManager {

  public ManagerStatus MStatus;
  public static DemoScenes CurrentScene;
  public static DemoScenes EndScene;
  public LoadStatus LStatus;
  // public Animator transition;
  public static Action<Scenes> sceneLoaded;

  private IEnumerator SceneIter;

  public void StartUp() {     // from IManager



    Debug.Log("Scene Manager is starting ... ");

    // // rough collection of scenes, may need to be a List later
    DemoScenes[] SceneArray = {DemoScenes.MiniGameMenu};
    
    // // grab the iterator
    this.SceneIter = SceneArray.GetEnumerator(); 
  

    // // set the statuses
    this.MStatus = ManagerStatus.ON;
    CurrentScene = DemoScenes.Sprint2DemoMenu;
    EndScene = DemoScenes.MiniGameMenu;
    this.LStatus = LoadStatus.COMPLETE;

  }

  private IEnumerator Next() {  

    // // shift to the next scene in the iterator and hold on to it
    this.SceneIter.MoveNext();
    
    // // try to grab the current Scene from iterator
    DemoScenes next;
    try {
      next = (DemoScenes) this.SceneIter.Current;
    }
    catch(InvalidOperationException e) { 
      Debug.Log($"{e.Message}: Next Scene is not available");
      yield break;
    }

    // set the load status
    this.LStatus = LoadStatus.LOADING;

    Debug.Log($"Loading {next} ... ");
    
    // set the current scene
    CurrentScene = next;

    // "Start" condition to transition fades start fade -> end fade
    // this.transition.SetTrigger("Start");

    // pause only this routine
    yield return new WaitForSeconds(1f);   


    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;
    Debug.Log($"Loading {next} is complete ... ");

  }

  private IEnumerator Next(DemoScenes next) {
    
    // set the load status
    this.LStatus = LoadStatus.LOADING;

    Debug.Log($"Loading {next} ... ");

    // set the current scene
    CurrentScene = next;

    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;
    Debug.Log($"Loading {next} is complete ... ");

    yield return null;

  }

  public void NextScene() {
    StartCoroutine(Next());
  }

  public void NextScene(DemoScenes s) {
    StartCoroutine(Next(s));
  }

  public void UnloadSceneAsync(DemoScenes scene) {
    SceneManager.UnloadSceneAsync($"{scene}");
  }

  


  public bool isLoadingComplete() {
    if (this.LStatus == LoadStatus.COMPLETE) {
      return true;
    }
    return false;
  }

  public void SetAnimator(Animator transition) {
    // this.transition = transition;
  }
  
}

