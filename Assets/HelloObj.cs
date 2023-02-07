using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelloObj : MonoBehaviour {
    
    void Awake() {
      MySceneManager.sceneLoaded += OnSceneLoad;
    }

    // should turn into a Coroutine()?
    void OnSceneLoad(Scenes scene) {
    
      // loading complete, now unsubscribe
      MySceneManager.sceneLoaded -= OnSceneLoad;

      Debug.Log($"Hello from {scene}!");
    }

     // Start is called before the first frame update
    void Start() {
      OnSceneLoad(MySceneManager.CurrentScene);  
    }

    // Update is called once per frame
    void Update() {}
}
