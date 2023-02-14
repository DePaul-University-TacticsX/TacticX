using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(MySceneManager))]     

public class TXGameManager : MonoBehaviour {

  // only need one shared scene manager and manager execution order (for now)
  private static MySceneManager scenes;
  private static List<IManager> ManagerOrder;
  

  void Awake() {
    DontDestroyOnLoad(this.gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)
    
    // grab the Scene Manager object in Scene1
    scenes = GetComponent<IterSceneManager>();    // IterScene inherits MySceneManager

    // add the managers to the list
    ManagerOrder = new List<IManager>();
    ManagerOrder.Add(scenes);
    
    // call StartupManagers
    StartCoroutine(StartupManagers());

  }

  private IEnumerator StartupManagers() {
    Debug.Log("Starting all Game Managers ... ");
    
    // call startup on all managers
    foreach (IManager mgr in ManagerOrder) {
      mgr.StartUp();
    }   

    // TODO: implement a check on the manager's status, before completing this?

    Debug.Log("... all Managers have started");

    // yield tells Coroutines to temporarily pause, returning control to other unity processes, then pick up again in the future
    yield return null;   

  }

  // Update is called once per frame
  void Update() {
  
    // switch scenes on a rough timer
    // NextScene handles an exception, if there are no more scenes left 
    if (scenes.GetCurrent() != scenes.GetEnd()) {
      
      // shift about every 20 secs if its 60fps?
      // also is there a scene currently loading
      if (Time.frameCount % 600 == 0 && scenes.isLoadingComplete()) {   
        scenes.NextScene();
      }
    }

  }

  public static MySceneManager GetScenes() {
    return scenes;
  } 
}