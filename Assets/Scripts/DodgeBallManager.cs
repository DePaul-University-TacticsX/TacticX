using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBallManager : MonoBehaviour
{
    public DodgeBallState State;

    public static DodgeBallManager manager;
    public static event Action<DodgeBallState> OnStateChanged;


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

    public void UpdateDodgeBallState(DodgeBallState newState)
    {
        State = newState;

        switch (newState)
        {
            case DodgeBallState.MovePlayer:
                MovePlayerHandler();
                break;
            case DodgeBallState.Win:
                HandleWin();
                break;
            case DodgeBallState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleWin()
    {
        //logic to place powerup into players inventory
        //FindObjectOfType<PowerUpManager>().PowerUpManager(PowerUpState.PowerUpEarned);
        Instantiate(Resources.Load<GameObject>("Win Prefab"));
    }

    private void HandleLose()
    {
        //logic to penilize player for losing minigame
        //FindObjectOfType<PowerUpManager>().PowerUpManager(PowerUpState.PowerUpLost);
        Instantiate(Resources.Load<GameObject>("Lose Prefab"));
    }

    private void MovePlayerHandler()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateDodgeBallState(DodgeBallState.MovePlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum DodgeBallState
{
    MovePlayer,
    Win,
    Lose
}
