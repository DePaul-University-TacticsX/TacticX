using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 1f;
    public float openingMinHeight = -100f;
    public float openingMaxHeight = 100f;

    private void OnEnable() 
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        if (FindObjectOfType<FlappyBirdManager>().State == FlappyBirdState.MovePlayer)
        {
            GameObject pipes = Instantiate(pipePrefab, transform.position, Quaternion.identity);
            pipes.transform.Find("opening").transform.position += Vector3.up * Random.Range(openingMinHeight, openingMaxHeight);
            pipes.transform.SetParent(transform, true);
            pipes.transform.localScale = new Vector3(1, 1, 1);
        }
    }


}
