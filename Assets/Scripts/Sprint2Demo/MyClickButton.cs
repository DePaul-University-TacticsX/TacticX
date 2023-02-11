using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyClickButton : MonoBehaviour {

  public DemoScenes scene;

  public void MyClick() {
    TXDemoManager.GetScenes().NextScene(scene);
  }
}
