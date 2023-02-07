using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObj : MonoBehaviour {
    public Animator transition;
    
    // Start is called before the first frame update
    void Start() {

      // The Crossfade Animator prefab is a GameObject child of the FadeObj gameobj (that contains this script)
      // this specifically sets transition to that specific Animator object in the scene
      this.transition = gameObject.GetComponentInChildren<Animator>();
      
      TXGameManager.GetScenes().SetAnimator(this.transition);
    }

}
