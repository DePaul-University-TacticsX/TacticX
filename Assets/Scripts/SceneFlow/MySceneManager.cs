using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class
using System;   // for System.InvalidOperationException


public class MySceneManager : MonoBehaviour, IManager {

  public ManagerStatus MStatus;
  public Scenes CurrentScene;
  public Scenes EndScene;
  public LoadStatus LStatus;
  public Animator transition;
  public static Action<Scenes> sceneLoaded;

  public IEnumerator SceneIter;

  public Scenes GetCurrent() {
    return this.CurrentScene;
  }

  public Scenes GetEnd() {
    return this.EndScene;
  }  

  virtual public void StartUp() {     // from IManager

    Debug.Log("Sprint2 Scene Manager is starting ... ");

    // set the statuses
    this.MStatus = ManagerStatus.ON;
    CurrentScene = Scenes.Sprint2DemoMenu;
    
    this.LStatus = LoadStatus.COMPLETE;

  }

  // Next() is called in TXGameManager and has transition elements added
  private IEnumerator Next() {  

    // shift to the next scene in the iterator and hold on to it
    this.SceneIter.MoveNext();
    
    // try to grab the current Scene from iterator
    Scenes next;
    try {
      next = (Scenes) this.SceneIter.Current;
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

    if (this.transition is null) {
      // TODO: throw a custom no transiton Exception instead?
      Debug.Log("Transition object is not set!");
    }
    // "Start" condition to transition fades start fade -> end fade
    this.transition?.SetTrigger("Start");

    // pause only this routine
    yield return new WaitForSeconds(1f);

    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;

  }

  public void NextScene() {
    StartCoroutine(Next());
  }

  // Next(next) is called in TXDemoManager, with no transition elements yet
  private IEnumerator Next(Scenes next) {
    
    // set the load status
    this.LStatus = LoadStatus.LOADING;

    Debug.Log($"Loading {next} ... ");

    // set the current scene
    CurrentScene = next;

    // TODO: add fade transition here

    // actually load it in Unity
    SceneManager.LoadScene($"{next}");   // must also add the new scene to the build settings for this to run

    // loading has completed
    this.LStatus = LoadStatus.COMPLETE;

    yield return null;

  }

  public void AddScene(Scenes scene) {
    
    this.LStatus = LoadStatus.LOADING;

    Debug.Log($"Adding scene {scene} ... ");

    CurrentScene = scene;

    SceneManager.LoadScene($"{scene}", LoadSceneMode.Additive);

    this.LStatus = LoadStatus.COMPLETE;

  }

  public void SetActive(Scenes scene) {
    SceneManager.SetActiveScene(SceneManager.GetSceneByName($"{scene}"));
  }

  public void NextScene(Scenes s) {
    StartCoroutine(Next(s));
  }

  public void UnloadSceneAsync(Scenes scene) {
    SceneManager.UnloadSceneAsync($"{scene}");
  }

  public bool isLoadingComplete() {
    if (this.LStatus == LoadStatus.COMPLETE) {
      return true;
    }
    return false;
  }

  public void SetAnimator(Animator transition) {
    this.transition = transition;
  }
  
}

