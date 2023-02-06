using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour
{

    [SerializeField]
    Button btn;

    private void Awake()
    {
        btn.onClick.AddListener(OnButtonPress);
    }
    
    public void OnButtonPress()
    {
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.PlayMiniGame);
    }
}
