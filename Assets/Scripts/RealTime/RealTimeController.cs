using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeController : MonoBehaviour
{

    AudioSource _audiosource;

    // Start is called before the first frame update
    void Start() {
      _audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move is always tested for.
        RTManager.getActive().Move();

        // if user tries to swap, go ahead and swap
        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     RTManager.next_in_line();
        // }



        // Update for Attack
        if (Input.GetKeyDown(KeyCode.Space)) {
          
          // RTManager.getActive().Attack();

          _audiosource.Play();
          EnemyManager.AttackEnemy(this.transform);

          

        }
    }
}
