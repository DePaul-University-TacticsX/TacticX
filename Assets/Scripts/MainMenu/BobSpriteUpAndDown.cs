using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobSpriteUpAndDown : MonoBehaviour
{
    public float speed = 1.0f;   // The speed of the bobbing motion
    public float height = 100f;  // The height of the bobbing motion

    private Vector3 startPosition;

    void Start()
    {
        // Save the starting position of the sprite
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the y position of the sprite using a sine wave
        float yPos = Mathf.Sin(Time.time * speed) * height;

        // Move the sprite to the new position
        transform.position = startPosition + new Vector3(0, yPos, 0);
    }
}