using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Image m_Image;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;
    private Vector3 direction;
    public float gravity = -9.8f;

    public float strength = 50f;

    private bool throughOpening = false;


    private void Awake()
    {
     
    }
    private void Start()
    {
        StartCoroutine(Func_PlayAnimUI());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            m_IndexSprite = 0;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<FlappyBirdManager>().State == FlappyBirdState.MovePlayer)
        {
            if (other.gameObject.name == "topBound")
            {
                direction = Vector3.down * 200f;
                direction.y += gravity * Time.deltaTime;
                transform.position += direction * Time.deltaTime;
            }
            if (other.gameObject.name == "bottomBound")
            {
                direction = Vector3.up * 200f;
                direction.y += gravity * Time.deltaTime;
                transform.position += direction * Time.deltaTime;
            }
            if (other.gameObject.name == "opening")
            {
                throughOpening = true;
                StartCoroutine(pipeTriggerDelay());
            }
            if (other.gameObject.name == "pipePart" && throughOpening == false)
             {
                FindObjectOfType<FlappyBirdManager>().UpdateFlappyBirdState(FlappyBirdState.Lose);
                Destroy(this);
             }
        }
    }

    IEnumerator pipeTriggerDelay()
    {
            yield return new WaitForSeconds(0.035f);
            throughOpening = false;

    }
}
