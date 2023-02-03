using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(MySceneManager))]     

public class GManager : MonoBehaviour {

  public static MySceneManager scenes;
  List<IManager> ManagerOrder;

  void Awake() {
    DontDestroyOnLoad(gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)
    
    scenes = GetComponent<MySceneManager>();

    this.ManagerOrder = new List<IManager>();
    this.ManagerOrder.Add(scenes);
    
    StartCoroutine(StartupManagers());

  }

  private IEnumerator StartupManagers() {
    Debug.Log("Starting all Game Managers ... ");
    foreach (IManager mgr in this.ManagerOrder) {
      mgr.StartUp();
    }   

    // TODO: implement a check on the manager's status, before completing this?

    Debug.Log("... all Managers have started");

    yield return null;
  }

  // Update is called once per frame
  void Update() {
  
    if (Time.frameCount % 1200 == 0) {    // shift about every 20 secs if its 60fps?
      scenes.NextScene();
    }
  }


}