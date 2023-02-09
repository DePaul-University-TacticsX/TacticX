using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightsScript : MonoBehaviour
{
    [SerializeField] private Image redLight;
    [SerializeField] private Image greenLight;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeLightColor", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeLightColor()
    {
        float delay = Random.Range(2.2f, 5.0f);

        if (greenLight.color == Color.green)
        {
            greenLight.color = Color.gray;
            redLight.color = Color.red;
        }
        else
        {
            greenLight.color = Color.green;
            redLight.color = Color.gray;
        }

        Invoke("ChangeLightColor", delay);
       

    }
}
