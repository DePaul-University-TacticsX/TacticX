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

    //// Update is called once per frame
    //void Update()
    //{
    //    //if (FindObjectOfType<RedLightGreenLightManager>().State != RLGLState.MovePlayer)
    //    //{
    //    //    return;
    //    //}
    //    //else
    //    //if (Input.GetKey(KeyCode.LeftArrow))
    //    //{
    //    //        transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed), transform.position.y, transform.position.z);
    //    //}
    //    //if (Input.GetKey(KeyCode.RightArrow))
    //    //{
    //    //        transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed), transform.position.y, transform.position.z);
    //    //}
    //    // Keyboard Input (Arrows)

    //    Vector2 move = new Vector2(0, 0);
    //    if (Input.GetKey(KeyCode.UpArrow)) { move.y += speed; }
    //    if (Input.GetKey(KeyCode.DownArrow)) { move.y -= speed; }
    //    if (Input.GetKey(KeyCode.LeftArrow)) { move.x -= speed; }
    //    if (Input.GetKey(KeyCode.RightArrow)) { move.x += speed; }
    //    transform.anchoredPosition += move;

    //    // Position clamping

    //    Vector2 clamped = transform.anchoredPosition;
    //    clamped.x = Mathf.Clamp(clamped.x, transform.rect.width / 2, canvasRect.width - transform.rect.width / 2);
    //    clamped.y = Mathf.Clamp(clamped.y, transform.rect.height / 2, canvasRect.height - transform.rect.height / 2);
    //    transform.anchoredPosition = clamped;
    //}

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