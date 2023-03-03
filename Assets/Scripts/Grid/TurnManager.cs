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
        Template.gameObject.transform.SetParent(null);
    }

    
    public static void NextTurn()
    {
        instance.privNextTurn(true);
    }

    public static void AddParticipant(GamePiece gamePiece,Sprite image,bool isAI)
    {
        if (image == null) image = instance.defaultSprite;

        instance.participants.Add(new Turn(gamePiece, image, isAI));
    }

    public static void RemoveParticipant(GamePiece gamePiece)
    {
        instance.privRemoveParticipant(gamePiece);        
    }

    public static Turn GetCurrentTurn()
    {
        return instance.currentTurn;
    }

    public static void Build(bool notifyListeners)
    {
        instance.privBuild(notifyListeners);
    }

    public static void AddTurnChangedObserver(Action<Turn> onTurnChanged)
    {
        instance.TurnChangedAction += onTurnChanged;
    }

    public static void RemoveTurnChangedObserver(Action<Turn> onTurnChanged)
    {
        instance.TurnChangedAction -= onTurnChanged;
    }

    private void privBuild(bool notifyListeners)
    {
        for(int i = 0; i < cycles; i++)
        {
            for(int j = 0; j < participants.Count; j++)
            {
                EnqueueTurn(participants[j]);
            }
        }

        currentTurn = queue.Dequeue();
        //Template.gameObject.transform.SetParent(null);
        //Destroy(Template.gameObject);
        if(notifyListeners) TurnChangedAction?.Invoke(currentTurn);
    }

    private void privNextTurn(bool notifyListeners)
    {
        Turn newTurn = currentTurn.Clone();
        queue.Enqueue(newTurn);
        Parent.GetChild(0).SetAsLastSibling();
        currentTurn = queue.Dequeue();
        if(notifyListeners) TurnChangedAction?.Invoke(currentTurn);
    }

    private void EnqueueTurn(Turn turn)
    {
        queue.Enqueue(turn.Clone());
        Image img = Instantiate(Template, Parent);
        img.sprite = turn.Sprite;
        img.gameObject.SetActive(true);
    }

    private void privRemoveParticipant(GamePiece gamePiece)
    {
        Queue<Turn> newQueue = new Queue<Turn>();
        
        while(currentTurn.GamePiece == gamePiece)
        {
            privNextTurn(false);
        }

        newQueue.Enqueue(currentTurn);    

        while(queue.Count > 0)
        {
            Turn t = queue.Dequeue();
            if (t.GamePiece != gamePiece) newQueue.Enqueue(t);
        }

        queue = newQueue;

        Rebuild();

        currentTurn = queue.Dequeue();

        TurnChangedAction?.Invoke(currentTurn);
    }

    void Rebuild()
    {
        int nQueue = queue.Count;
        int nChildren = Parent.childCount;

        for (int i = 0; i < nChildren; i++)
        {
            Destroy(Parent.GetChild(i).gameObject);
        }
         
        for (int i = 0; i < nQueue; i++)
        {
            Turn turn = queue.Dequeue();
            Image img = Instantiate(Template, Parent);
            img.sprite = turn.Sprite;
            img.gameObject.SetActive(true);
            queue.Enqueue(turn);
        }
    }
}
