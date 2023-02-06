using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{

    [SerializeField]
    Button btn;

    private void Awake()
    {
        btn.onClick.AddListener(OnButtonPress);
    }
    
    public void OnButtonPress()
    {
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.ExitMiniGameMenu);
    }
}
