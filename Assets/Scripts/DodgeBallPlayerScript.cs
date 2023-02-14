using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBallPlayerScript : MonoBehaviour
{
    private float speed = 200.0f;

    private Rigidbody2D rb;

    private bool touchingLeftBound = false;
    private bool touchingRightBound = false;

    private void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)==false)
        {
            if (touchingRightBound == false)
            {
                float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

                Vector2 newPosition = rb.position + Vector2.right * x;

                rb.MovePosition(newPosition);

                touchingLeftBound = false;
            }
        } 
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)==false) 
        {
            if (touchingLeftBound == false)
            {
                float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

                Vector2 newPosition = rb.position + Vector2.right * x;

                rb.MovePosition(newPosition);
  
                touchingRightBound = false;
            }
        }
    }

    private void Awake()
    {
    
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "leftBound")
        {
            touchingLeftBound = true;
        }
        if (other.gameObject.name == "rightBound")
        {
            touchingRightBound = true;
        }
        if (other.gameObject.name == "ball" || other.gameObject.name == "ball(Clone)")
        {
            if (FindObjectOfType<DodgeBallManager>().State == DodgeBallState.MovePlayer)
            {
                FindObjectOfType<DodgeBallManager>().UpdateDodgeBallState(DodgeBallState.Lose);
            }
                
        }
    }
}