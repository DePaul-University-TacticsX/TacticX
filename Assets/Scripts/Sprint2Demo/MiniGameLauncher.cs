using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameLauncher : MonoBehaviour {

  public void Clicked() {
    TXDemoManager.GetScenes().NextScene();
  }
}
