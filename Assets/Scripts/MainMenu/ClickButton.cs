using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour {
    public Scenes scene;

    public void MyClick() {
        TacticsXGameManager.GetScenes().NextScene(scene);
    }

    void Update()
    {
        //TODO Add sound effect to give player confirmation?
        //TODO Add proper fade transition to next scene
        if (Input.anyKeyDown)
            MyClick();
    }
}
