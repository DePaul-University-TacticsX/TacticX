using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Destroy(transform.gameObject.GetComponentInParent<Canvas>().gameObject);
        FindObjectOfType<MiniGameManager>().UpdateMiniGameState(MiniGameState.ExitMiniGameMenu);
    }
}
