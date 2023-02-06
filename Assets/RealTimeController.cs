using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeController : MonoBehaviour
{

    public RTManager manager;


    // Start is called before the first frame update
    void Start()
    {
        this.manager = new RTManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= -5.0f) {
            this.manager.getActive().changeDirection();
        }

        if (transform.position.z <= -10.0f) {
            this.manager.getActive().changeDirection();
        }

        this.manager.getActive().Move();
    }
}
