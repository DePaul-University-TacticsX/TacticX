using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdManager : MonoBehaviour
{
    public FlappyBirdState State;

    public static FlappyBirdManager manager;
    public static event Action<FlappyBirdState> OnStateChanged;


    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            //DontDestroyOnLoad(this);
        }
        else if (manager == this)
        {
            Destroy(gameObject);
        }

    }

    public void UpdateFlappyBirdState(FlappyBirdState newState)
    {
        State = newState;

        switch (newState)
        {
            case FlappyBirdState.MovePlayer:
                MovePlayerHandler();
                break;
            case FlappyBirdState.Win:
                HandleWin();
                break;
            case FlappyBirdState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleWin()
    {
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.MiniGameWon);
        Instantiate(Resources.Load<GameObject>("Win Prefab"));
        Destroy(gameObject);
    }

    private void HandleLose()
    {
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.MiniGameLost);
        Instantiate(Resources.Load<GameObject>("Lose Prefab"));
        Destroy(gameObject);
    }

    private void MovePlayerHandler()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateFlappyBirdState(FlappyBirdState.MovePlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum FlappyBirdState
{
    MovePlayer,
    Win,
    Lose
}
