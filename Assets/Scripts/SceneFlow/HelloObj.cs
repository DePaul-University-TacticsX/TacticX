using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelloObj : MonoBehaviour, IOnSceneLoad {
    
    void Awake() {
      MySceneManager.sceneLoaded += OnSceneLoad;
    }

    // should turn into a Coroutine()?
    public void OnSceneLoad(Scenes scene) {     // IOnSceneLoad
    
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
