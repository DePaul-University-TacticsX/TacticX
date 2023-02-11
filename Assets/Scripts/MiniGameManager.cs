using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private GameObject QuitButton;
    public static event Action<MiniGameState> OnStateChanged;
    public static MiniGameManager manager;
    public MiniGameState State;
    public int selectedMiniGame;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            //DontDestroyOnLoad(this);
        } else if (manager == this)
        {
            Destroy(gameObject);
        }
        
    }

    public void UpdateMiniGameState(MiniGameState newState)
    {
        State = newState;

        switch (newState)
        {
            case MiniGameState.LoadMiniGameInstructions:
                LoadMiniGameInstructionsHandler();
                break;
            case MiniGameState.PlayMiniGame:
                PlayMiniGameHandler();
                break;
            case MiniGameState.ExitMiniGameMenu:
                ExitMenuHandler();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }

        OnStateChanged?.Invoke(newState);
    }

    private void LoadMiniGameInstructionsHandler()
    {
        selectedMiniGame = UnityEngine.Random.Range(1, 4);
        if (selectedMiniGame == 1)
        {
            Instantiate(Resources.Load<GameObject>("Red Light Green Light Instructions Prefab"));
        }
        else if (selectedMiniGame == 2)
        {
            Instantiate(Resources.Load<GameObject>("Dodgeball Instructions Prefab"));
        }
        else if (selectedMiniGame == 3)
        {
            Instantiate(Resources.Load<GameObject>("Flappybird Instructions Prefab"));
        }
    }

    private void ExitMenuHandler()
    {
        Debug.Log("Todo - escape mini game menu and go back to main game.");
        //FindObjectOfType<MainGameManager>().MainGameManager(MainGameState.LeaveMiniGame);
    }

    private void PlayMiniGameHandler()
    {
        if (selectedMiniGame == 1)
        {
            Instantiate(Resources.Load<GameObject>("Red Light Green Light Prefab"));
        } else if (selectedMiniGame == 2)
        {
            Instantiate(Resources.Load<GameObject>("Dodgeball Prefab"));
        }
         else if(selectedMiniGame == 3)
        {
            Instantiate(Resources.Load<GameObject>("Flappybird Prefab"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //load minigame 
    //minigame.gamestatechanged += onGameWin => close minigame load win screen 
}

public enum MiniGameState
{
    LoadMiniGameInstructions,
    PlayMiniGame, 
    ExitMiniGameMenu
}
