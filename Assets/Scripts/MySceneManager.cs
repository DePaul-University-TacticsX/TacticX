using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;    // contains SceneManger class

public class MySceneManager : MonoBehaviour, IManager {

  public ManagerStatus status;
  public Scene CurrentScene;
  public Scene EndScene;

  public void StartUp() {
    Debug.Log("Scene Manager is starting ... ");

    this.CurrentScene = Scene.Scene1;
    this.EndScene = Scene.Scene3;

    this.status = ManagerStatus.ON;

    this.TransitionScenes();

  }

  public ManagerStatus GetStatus() {
    return this.status;
  }

  public void NextScene(Scene NewScene) {
    Debug.Log($"Loading {NewScene} ... ");
    SceneManager.LoadScene($"{NewScene}");   // must also add the new scene to the build settings for this to run
  }

  private void TransitionScenes() {
    Debug.Log("Beginning transition ... ");

    Scene[] scenes = {Scene.Scene2, Scene.Scene3};
    foreach (Scene s in scenes) {
      this.NextScene(s);
      this.CurrentScene = s;
    }
    Debug.Log("All scenes have completed");
  }

}