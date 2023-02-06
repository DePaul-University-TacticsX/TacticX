using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 30f;

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime * speed), transform.position.z);
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
