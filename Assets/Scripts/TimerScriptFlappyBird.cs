using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScriptFlappyBird : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 38f;
    float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        } else
        {
            if (FindObjectOfType<FlappyBirdManager>().State == FlappyBirdState.MovePlayer)
            {
                FindObjectOfType<FlappyBirdManager>().UpdateFlappyBirdState(FlappyBirdState.Win);
            }
        }
    }
}
