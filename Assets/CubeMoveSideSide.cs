using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoveCircle : MonoBehaviour
{

    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // we want to see the cube go back and forth.
        // The Translate method accepts a value to transform the pre-existing value by.
        // important note: 0 implies no change.
        // combining this translation with a rotation creates a rough circular motion.
        if (transform.position.z >= -5.0f) {
            this.direction = -1;
        }

        if (transform.position.z <= -10.0f) {
            this.direction = 1;
        }

        transform.Translate(new Vector3(0, 0, direction * Time.deltaTime));
    }
}
