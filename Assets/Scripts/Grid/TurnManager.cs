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
    private Queue<Participant> queue = new Queue<Participant>();
    private List<Participant> participants = new List<Participant>();
    private Participant currentTurn;

    private Action<Participant> TurnChangedAction;

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

        instance.participants.Add(new Participant(gamePiece, image, isAI));
    }

    public static void RemoveParticipant(GamePiece gamePiece)
    {
        instance.privRemoveParticipant(gamePiece);        
    }

    public static Participant GetCurrentTurn()
    {
        return instance.currentTurn;
    }

    public static Participant FindParticipant(GamePiece gamePiece)
    {
        return instance.participants.Find(p => p.GamePiece == gamePiece);
    }

    public static void Build(bool notifyListeners)
    {
        instance.privBuild(notifyListeners);
    }

    public static void AddTurnChangedObserver(Action<Participant> onTurnChanged)
    {
        instance.TurnChangedAction += onTurnChanged;
    }

    public static void RemoveTurnChangedObserver(Action<Participant> onTurnChanged)
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
        Participant newTurn = currentTurn.Clone();
        queue.Enqueue(newTurn);
        Parent.GetChild(0).SetAsLastSibling();
        currentTurn = queue.Dequeue();
        if(notifyListeners) TurnChangedAction?.Invoke(currentTurn);
    }

    private void EnqueueTurn(Participant turn)
    {
        queue.Enqueue(turn.Clone());
        Image img = Instantiate(Template, Parent);
        img.sprite = turn.Sprite;
        img.gameObject.SetActive(true);
    }

    private void privRemoveParticipant(GamePiece gamePiece)
    {
        Queue<Participant> newQueue = new Queue<Participant>();
        
        while(currentTurn.GamePiece == gamePiece)
        {
            privNextTurn(false);
        }

        newQueue.Enqueue(currentTurn);    

        while(queue.Count > 0)
        {
            Participant t = queue.Dequeue();
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
            Participant turn = queue.Dequeue();
            Image img = Instantiate(Template, Parent);
            img.sprite = turn.Sprite;
            img.gameObject.SetActive(true);
            queue.Enqueue(turn);
        }
    }
}
