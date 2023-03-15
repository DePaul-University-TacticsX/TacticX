using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RedLightGreenLightManager : MonoBehaviour
{
    [SerializeField] private Image redLight;
    [SerializeField] private Image greenLight;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finish;
    public GameObject LosePrefab, WinPrefab;
    public RLGLState State;

    public static RedLightGreenLightManager manager;
    public static event Action<RLGLState> OnStateChanged;


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

    public void UpdateRLGLState(RLGLState newState)
    {
        State = newState;

        switch(newState)
        {
            case RLGLState.MovePlayer:
                MovePlayerHandler();
                break;
            case RLGLState.Win:
                HandleWin();
                break;
            case RLGLState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnStateChanged?.Invoke(newState);
    }

    private void HandleWin()
    {
        WinPrefab = Resources.Load<GameObject>("Win Prefab");
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.MiniGameWon);
        Instantiate(WinPrefab);
        Destroy(gameObject);
    }

    private void HandleLose()
    {
        LosePrefab = Resources.Load<GameObject>("Lose Prefab");
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.MiniGameLost);
        Instantiate(LosePrefab);
        Destroy(gameObject);
    }

    private void MovePlayerHandler()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateRLGLState(RLGLState.MovePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(State != RLGLState.MovePlayer)
        {
            return;
        }
        else
        {
            if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.UpArrow)) && redLight.color == Color.red)
            {
                UpdateRLGLState(RLGLState.Lose);
            }
        }
    }
}

public enum RLGLState
{
    MovePlayer,
    Win,
    Lose
}