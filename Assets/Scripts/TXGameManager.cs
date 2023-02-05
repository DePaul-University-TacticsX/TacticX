using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(MySceneManager))]     

public class TXGameManager : MonoBehaviour {

  public static MySceneManager scenes;
  List<IManager> ManagerOrder;

  void Awake() {
    DontDestroyOnLoad(gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)
    
    // grab the Scene Manager object in Scene1
    scenes = GetComponent<MySceneManager>();

    // add the managers to the list
    this.ManagerOrder = new List<IManager>();
    this.ManagerOrder.Add(scenes);
    
    // call StartupManagers
    StartCoroutine(StartupManagers());

  }

  private IEnumerator StartupManagers() {
    Debug.Log("Starting all Game Managers ... ");
    
    // call startup on all managers
    foreach (IManager mgr in this.ManagerOrder) {
      mgr.StartUp();
    }   

    // TODO: implement a check on the manager's status, before completing this?

    Debug.Log("... all Managers have started");

    yield return null;
  }

  // Update is called once per frame
  void Update() {
  
    // switch scenes on a timer
    // if (Time.frameCount % 1200 == 0) {    // shift about every 20 secs if its 60fps?
    //   scenes.NextScene();
    // }

    // bad if condition, maybe edit NextScene()?
    if (scenes.GetLoadStatus() == LoadStatus.COMPLETE && scenes.CurrentScene != Scenes.Scene3) {
      Debug.Log($"Loading next scene at frame: {Time.frameCount}");   // 
      scenes.NextScene();
    }
  }
  

}