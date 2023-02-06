using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBallPlayerScript : MonoBehaviour
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
        //if (FindObjectOfType<RedLightGreenLightManager>().State != RLGLState.MovePlayer)
        //{
        //    return;
        //}
        //else
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                    transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed), transform.position.y, transform.position.z);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                    transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed), transform.position.y, transform.position.z);
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "rightCollider" || other.gameObject.name == "leftCollider")
        {
            Debug.Log("stop movement");
        }
    }
}