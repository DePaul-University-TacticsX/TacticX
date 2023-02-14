using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManager class
using System;

public class IterSceneManager : MySceneManager {

  override public void StartUp() {     // from IManager

    Debug.Log("Scene Manager is starting at Scene 1 ... ");

    // rough collection of scenes, may need to be a List later
    Scenes[] SceneArray = {Scenes.Scene2, Scenes.Scene3};
    
    // grab the iterator
    this.SceneIter = SceneArray.GetEnumerator(); 

    // set the statuses
    this.MStatus = ManagerStatus.ON;
    CurrentScene = Scenes.Scene1;
    EndScene = Scenes.Scene3;
    this.LStatus = LoadStatus.COMPLETE;

  }


}

