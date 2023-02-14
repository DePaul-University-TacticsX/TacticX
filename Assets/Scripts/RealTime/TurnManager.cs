using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Transform Parent;
    public Image Template;
    public Sprite[] Images;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++) 
        {
            Image img = Instantiate(Template, Parent);
            img.sprite = Images[i % 2] ;
            
        }
        Template.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Next Turn")]
    public void NextTurn()
    {
        Parent.GetChild(0).SetAsLastSibling();
    }
}
