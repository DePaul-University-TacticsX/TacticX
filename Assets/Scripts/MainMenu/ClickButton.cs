using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {
    public Scenes scene;
    public bool anyKeyEnabled;

    public void MyClick() {
        TacticsXGameManager.GetScenes().NextScene(scene);
    }

    void Update()
    {
        //TODO Add sound effect to give player confirmation?
        //TODO Add proper fade transition to next scene
        if (anyKeyEnabled && Input.anyKeyDown) MyClick();
    }
}
