using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {

  public Scenes scene;

  public void MyClick() {
    TacticsXGameManager.GetScenes().NextScene(scene);
  }
}
