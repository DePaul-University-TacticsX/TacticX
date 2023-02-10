using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(DemoSceneManager))]     

public class TXDemoManager : MonoBehaviour {

  // only need one shared scene manager and manager execution order (for now)
  private static DemoSceneManager scenes;
  private static List<IManager> ManagerOrder;
  

  void Awake() {
    DontDestroyOnLoad(this.gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)
    
    // // grab the Scene Manager object in Scene1
    scenes = GetComponent<DemoSceneManager>();

    // // add the managers to the list
    ManagerOrder = new List<IManager>();
    ManagerOrder.Add(scenes);
    
    // // call StartupManagers
    StartCoroutine(StartupManagers());

  }

  private IEnumerator StartupManagers() {
    // Debug.Log("Starting all Game Managers ... ");
    
    // // call startup on all managers
    foreach (IManager mgr in ManagerOrder) {
      mgr.StartUp();
    }   

    // // TODO: implement a check on the manager's status, before completing this?

    Debug.Log("... all Managers have started");

    // // yield tells Coroutines to temporarily pause, returning control to other unity processes, then pick up again in the future
    yield return null;   

  }


  public static DemoSceneManager GetScenes() {
    return scenes;
  } 
}