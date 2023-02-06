using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 15f;
    float timeLeft;
    bool hasWon;
    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
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
            hasWon= true;
            Time.timeScale = 0;
        }
    }
}
