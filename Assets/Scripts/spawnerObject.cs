using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnerObject : MonoBehaviour
{
    public GameObject ball;
    private float speed = 100f;
    private Rigidbody2D rb;
    private bool switchDirection = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        float x = Time.fixedDeltaTime * speed;
        Vector2 newPosition;


        if (switchDirection)
        {
            newPosition = rb.position + Vector2.right * x;
        }
        else
        {
            newPosition = rb.position + Vector2.left * x;
        }

        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "leftBound")
        {
            switchDirection = true;
            speed = Random.Range(100f, 500f);
        }
        if (other.gameObject.name == "rightBound")
        {
            switchDirection = false;
            speed = Random.Range(100f, 500f);

        }
    }

    void spawnBall()
    {
        randomizeColor();
        GameObject newBall = Instantiate(ball, transform.position, transform.rotation);
        newBall.transform.SetParent(transform, true);
        newBall.transform.localScale = new Vector3(1, 1, 1);

    }

    void randomizeColor()
    {
        Color[] colors = { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };
        int i = Random.Range(0, 5);
        ball.GetComponent<Image>().color = colors[i];
        ball.GetComponent<Rigidbody2D>().gravityScale = Random.Range(6, 15);
        //ball.transform.SetSiblingIndex(1);

    }

    IEnumerator RandomSpawnTimer()
    {
        while (true)
        {
            spawnBall();
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        }
    }
}
