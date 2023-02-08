using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 15f;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<RedLightGreenLightManager>().State != RLGLState.MovePlayer)
        {
            return;
        }
        else
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.UpArrow))
        {
            float x = Time.fixedDeltaTime * speed;

            Vector3 newPosition = transform.position + Vector3.up * x;

            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Finish")
        {
            FindObjectOfType<RedLightGreenLightManager>().UpdateRLGLState(RLGLState.Win);
        }
    }
}
