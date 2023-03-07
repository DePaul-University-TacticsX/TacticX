using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    public Transform lookAt;
    [HideInInspector] public Transform local;

    // Start is called before the first frame update
    void Start()
    {
        local = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAt) {
            local.LookAt(2 * local.position - lookAt.position);
        }
    }
}
