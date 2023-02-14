using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(MySceneManager))]     

public class TXDemoManager : MonoBehaviour {

  // only need one shared scene manager and manager execution order (for now)
  private static MySceneManager scenes;
  private static List<IManager> ManagerOrder;
  

  void Awake() {
    DontDestroyOnLoad(this.gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)
    
    // grab the Scene Manager object in Scene1
    scenes = GetComponent<MySceneManager>();

    // add the managers to the list
    ManagerOrder = new List<IManager>();
    ManagerOrder.Add(scenes);
    
    // call StartupManagers
    StartCoroutine(StartupManagers());

  }

  private IEnumerator StartupManagers() {
    // Debug.Log("Starting all Game Managers ... ");
    
    // call startup on all managers
    foreach (IManager mgr in ManagerOrder) {
      mgr.StartUp();
    }   

    // TODO: implement a check on the manager's status, before completing this?

    Debug.Log("... all Managers have started");

    // yield tells Coroutines to temporarily pause, returning control to other unity processes, then pick up again in the future
    yield return null;   

  }

  void Update() {
    
    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.M)) {
      scenes.NextScene(Scenes.Sprint2DemoMenu);
      
      // if the other game manager is running then destroy it
      GameObject OtherGame = GameObject.Find("GameManager");
      if (OtherGame is not null) {
        Destroy(OtherGame);
      }

      // destroys the old DemoManager obj 
      Destroy(this.gameObject);   
    
    }
  }


  public static MySceneManager GetScenes() {
    return scenes;
  } 
}