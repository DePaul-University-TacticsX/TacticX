using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TacticsX.SoundEngine;
using UnityEngine.Analytics;



  
[RequireComponent(typeof(MySceneManager))]

public class TacticsXGameManager : MonoBehaviour {

  // only need one shared scene manager and manager execution order (for now)
  private static MySceneManager scenes;
  private static List<IManager> ManagerOrder;


  void Awake()
  {
      DontDestroyOnLoad(this.gameObject);   // keeps a GManager object alive between scenes (normally they are destroyed)

      // grab the Scene Manager object in Scene1
      scenes = GetComponent<MySceneManager>();    // IterScene inherits MySceneManager

      // add the managers to the list
      ManagerOrder = new List<IManager>();
      ManagerOrder.Add(scenes);

      // call StartupManagers
      StartCoroutine(StartupManagers());


      MusicManager.Play(MusicType.Music_01);
      Analytics.CustomEvent("Game_Start");
    }

  private IEnumerator StartupManagers()
  {
      Debug.Log("Starting all Game Managers ... ");

      // call startup on all managers
      foreach (IManager mgr in ManagerOrder)
      {
          mgr.StartUp();
      }

      // TODO: implement a check on the manager's status, before completing this?

      Debug.Log("... all Managers have started");

      // yield tells Coroutines to temporarily pause, returning control to other unity processes, then pick up again in the future
      yield return null;

  }

  // Update is called once per frame
  void Update()
  {

      /*if (Input.GetKey(KeyCode.M))
      {
          // possibly remove an audio listener component? 

          scenes.NextScene(Scenes.Scene2);
          scenes.AddScene(Scenes.Scene3);
          scenes.SetActive(Scenes.Scene2);
      }*/

  }

  public static MySceneManager GetScenes()
  {
      return scenes;
  }
}
