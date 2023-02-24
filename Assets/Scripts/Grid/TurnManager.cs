using System;
using System.Collections;
using System.Collections.Generic;
using TacticsX.GridImplementation;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;

    [SerializeField] private Transform Parent;
    [SerializeField] private Image Template;
    [SerializeField] private Sprite defaultSprite;

    private int cycles = 4;
    private Queue<Turn> queue = new Queue<Turn>();
    private List<Turn> participants = new List<Turn>();
    private Turn currentTurn;

    private Action<Turn> TurnChangedAction;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Template.gameObject.SetActive(false);
    }

    
    public static void NextTurn()
    {
        instance.privNextTurn();
    }

    public static void AddParticipant(GamePiece gamePiece,Sprite image,bool isAI)
    {
        if (image == null) image = instance.defaultSprite;

        instance.participants.Add(new Turn(gamePiece, image, isAI));
    }

    public static Turn GetCurrentTurn()
    {
        return instance.currentTurn;
    }

    public static void Build()
    {
        instance.privBuild();
    }

    public static void AddTurnChangedObserver(Action<Turn> onTurnChanged)
    {
        instance.TurnChangedAction += onTurnChanged;
    }

    public static void RemoveTurnChangedObserver(Action<Turn> onTurnChanged)
    {
        instance.TurnChangedAction -= onTurnChanged;
    }

    private void privBuild()
    {
        for(int i = 0; i < cycles; i++)
        {
            for(int j = 0; j < participants.Count; j++)
            {
                EnqueueTurn(participants[j]);
            }
        }

        currentTurn = queue.Dequeue();
        Destroy(Template.gameObject);
        TurnChangedAction?.Invoke(currentTurn);
    }

    private void privNextTurn()
    {
        Turn newTurn = currentTurn.Clone();
        queue.Enqueue(newTurn);
        Parent.GetChild(0).SetAsLastSibling();
        currentTurn = queue.Dequeue();
        TurnChangedAction?.Invoke(currentTurn);
    }

    private void EnqueueTurn(Turn turn)
    {
        queue.Enqueue(turn.Clone());
        Image img = Instantiate(Template, Parent);
        img.sprite = turn.Sprite;
        img.gameObject.SetActive(true);
    }
}
